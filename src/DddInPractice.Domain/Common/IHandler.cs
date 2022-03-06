namespace DddInPractice.Domain.Common
{
    public interface IHandler<in T> where T : IDomainEvent
    {
        void Handle(T domainEvent);
    }
}
