using DddInPractice.Domain.Common;

namespace DddInPractice.Data.Common;

public abstract class RepositoryBase<T> : IRepository<T> where T : AggregateRootBase
{
    public T? GetById(long id)
    {
        using var dbContext = DataContextFactory.CreateDataContext();
        return dbContext.Find<T>(id);
    }

    public void Save(T entity)
    {
        using var dbContext = DataContextFactory.CreateDataContext();
        dbContext.Update(entity);
        dbContext.SaveChanges();
    }
}
