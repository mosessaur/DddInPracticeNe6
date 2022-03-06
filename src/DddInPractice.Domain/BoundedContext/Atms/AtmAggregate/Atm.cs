using System;
using DddInPractice.Domain.BoundedContext.Atms.Events;
using DddInPractice.Domain.Common;
using DddInPractice.Domain.SharedKernel;
using static DddInPractice.Domain.SharedKernel.Money;

namespace DddInPractice.Domain.BoundedContext.Atms.AtmAggregate
{
    public class Atm : AggregateRootBase
    {
        private const decimal CommissionRate = 0.01m;
        public Money MoneyInside { get; private set; } = None;
        public decimal MoneyCharged { get; private set; }

        public static decimal CalculateAmountWithCommission(decimal amount)
        {
            decimal commission = amount * CommissionRate;
            var lessThanCent = CommissionRate % 0.01m;
            if (lessThanCent > 0)
            {
                commission = commission - lessThanCent + 0.01m;
            }

            var amountWithCommission = amount + commission;
            return amountWithCommission;
        }

        public string CanTakeMoney(decimal amount)
        {
            if (amount <= 0m)
                return "Invalid amount";

            if (MoneyInside.Amount < amount)
                return "Not enough money inside";

            if(!MoneyInside.CanAllocate(amount))
                return "Not enough money inside";

            return string.Empty;
;        }

        public void TakeMoney(decimal amount)
        {
            var canAllocationMessage = CanTakeMoney(amount);
            if (canAllocationMessage != string.Empty)
                throw new InvalidOperationException(canAllocationMessage);

            var allocated = MoneyInside.Allocate(amount);
            MoneyInside -= allocated;

            decimal amountWithCommission = CalculateAmountWithCommission(amount);
            MoneyCharged += amountWithCommission;

            AddDomainEvent(new BalanceChangedEvent(amountWithCommission));
        }

        public void LoadMoney(Money money)
        {
            MoneyInside += money;
        }
    }
}
