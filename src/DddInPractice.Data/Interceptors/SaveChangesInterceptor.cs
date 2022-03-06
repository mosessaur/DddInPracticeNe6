using DddInPractice.Data.Common;
using DddInPractice.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DddInPractice.Data.Interceptors;



/* Unlike NHibernate that has predefined events for PostInsert/Update/Delete.
 * I needed to use an implementation of ISaveChangesInterceptor to capture DbContext.SaveChanges related events
 * This can be done in many different ways. One straight forward way is to override DbContext.SaveChanges & DbContext.SaveChangesAsync directly
 * Also note that this is state-full interceptor. Thus using lock to manage access to state (_aggregateRoots)
 * Read more about interceptors https://aka.ms/efcore-docs-interceptors
 */
// TODO: Cover by unit tests/integration tests
internal class SaveChangesInterceptor : Microsoft.EntityFrameworkCore.Diagnostics.SaveChangesInterceptor
{
    private readonly object _lock = new();
    private readonly IList<AggregateRootBase> _aggregateRoots = new List<AggregateRootBase>();

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null)
            throw new InvalidOperationException("DbContext is null");

        lock (_lock)
        {
            foreach (var entityEntry in eventData.Context.ChangeTracker.Entries())
            {
                switch (entityEntry.State)
                {
                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    case EntityState.Deleted:
                    case EntityState.Modified:
                    case EntityState.Added:
                    default:
                        AddAggregateRoot(entityEntry);
                        break;
                }
            }
        }

        return result;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        SavingChanges(eventData, result);
        return ValueTask.FromResult(result);
    }

    public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
    {
        lock (_lock)
        {
            foreach (var aggregateRoot in _aggregateRoots)
            {
                DispatchEvents(aggregateRoot);
            }

            //Clear _aggregateRoots after dispatching all events.
            _aggregateRoots.Clear();
        }

        return result;
    }

    public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result,
        CancellationToken cancellationToken = new())
    {
        SavedChanges(eventData, result);
        return ValueTask.FromResult(result);
    }

    private void AddAggregateRoot(EntityEntry entityEntry)
    {
        // Value objects are not aggregate roots. Skip
        var isValueObject = entityEntry.Entity is IValueObject;
        if (isValueObject) return;

        // Entity is an AggregateRootBase
        if (entityEntry.Entity is AggregateRootBase aggregateRoot)
        {
            _aggregateRoots.Add(aggregateRoot);
            return;
        }

        // Entity not an AggregateRootBase & not owned. Skip
        if (!entityEntry.Metadata.IsOwned())
        {
            return;
        }

        // Entity is owned. Find Ownership relation and get navigation value (Owner entity instance)
        var ownerFk = entityEntry.Metadata.FindOwnership();
        var fieldInfo = ownerFk?.DependentToPrincipal?.FieldInfo;

        //Get navigation value(Owner entity instance) 
        if (fieldInfo?.GetValue(entityEntry.Entity) is AggregateRootBase ownerAggregateRoot)
            _aggregateRoots.Add(ownerAggregateRoot);
    }

    private static void DispatchEvents(AggregateRootBase? aggregateRoot)
    {
        if (aggregateRoot is null)
            return;

        foreach (IDomainEvent domainEvent in aggregateRoot.DomainEvents)
        {
            DomainEventDispatcher.Dispatch(domainEvent);
        }

        aggregateRoot.ClearEvents();
    }
}
