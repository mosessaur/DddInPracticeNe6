using DddInPractice.Domain.BoundedContext.Atms.Events;
using DddInPractice.Domain.BoundedContext.Management.HeadOfficeAggregate;
using DddInPractice.Domain.Common;

namespace DddInPractice.Data.Management.EventHandlers;

internal class BalanceChangedEventHandler : IHandler<BalanceChangedEvent>
{
    public void Handle(BalanceChangedEvent domainEvent)
    {
        var repository = new HeadOfficeRepository();
        HeadOffice? headOffice = HeadOfficeInstance.Instance;

        if (headOffice is null)
            throw new InvalidOperationException("Head office instance is not initialized");
            
        headOffice.ChangeBalance(domainEvent.Delta);
        repository.Save(headOffice);
    }
}
