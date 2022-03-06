using DddInPractice.Domain.BoundedContext.Atms.AtmAggregate;
using DddInPractice.Domain.BoundedContext.SnackMachines.SnackMachineAggregate;
using DddInPractice.Domain.Common;
using DddInPractice.Domain.SharedKernel;
using static DddInPractice.Domain.SharedKernel.Money;

namespace DddInPractice.Domain.BoundedContext.Management.HeadOfficeAggregate
{
    public class HeadOffice : AggregateRootBase
    {
        public decimal Balance { get; private set; }
        public Money Cash { get; private set; } = None;

        public void ChangeBalance(decimal delta)
        {
            Balance += delta;
        }

        public virtual void UnloadCashFromSnackMachine(SnackMachine snackMachine)
        {
            Money money = snackMachine.UnloadMoney();
            Cash += money;
        }

        public virtual void LoadCashToAtm(Atm atm)
        {
            atm.LoadMoney(Cash);
            Cash = Money.None;
        }
    }
}
