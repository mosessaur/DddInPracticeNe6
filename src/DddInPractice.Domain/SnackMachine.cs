using System;
using System.Linq;

using static DddInPractice.Domain.Money;

namespace DddInPractice.Domain
{
    public sealed class SnackMachine : EntityBase
    {
        public Money MoneyInside { get; private set; } = None;
        public Money MoneyInTransaction { get; private set; } = None;

        //public SnackMachine(long id, Money moneyInside)
        //{
        //    Id = id;
        //    MoneyInside = moneyInside;
        //}

        //public SnackMachine(Money moneyInside)
        //{
        //    MoneyInside = moneyInside;
        //}

        //public SnackMachine()
        //{
        //}

        public void InsertMoney(Money money)
        {
            Money[] coinsAndNotes =
            {
                Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar
            };
            if (!coinsAndNotes.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money;
        }

        public void ReturnMoney()
        {
            MoneyInTransaction = None;
        }

        public void BuySnack()
        {
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }
    }
}
