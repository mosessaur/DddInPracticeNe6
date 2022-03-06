using DddInPractice.Domain.Common;

namespace DddInPractice.Domain.BoundedContext.Atms.Events
{
    public class BalanceChangedEvent : IDomainEvent
    {
        public decimal Delta { get; private set; }

        public BalanceChangedEvent(decimal delta)
        {
            Delta = delta;
        }
    }
}
