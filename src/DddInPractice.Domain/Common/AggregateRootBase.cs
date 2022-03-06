using System.Collections.Generic;

namespace DddInPractice.Domain.Common
{
    public abstract class AggregateRootBase : EntityBase
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        protected void AddDomainEvent(IDomainEvent newEvent)
        {
            _domainEvents.Add(newEvent);
        }

        public void ClearEvents() => _domainEvents.Clear();
    }
}
