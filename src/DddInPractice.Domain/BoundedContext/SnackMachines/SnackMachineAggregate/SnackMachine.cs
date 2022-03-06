using System;
using System.Collections.Generic;
using System.Linq;
using DddInPractice.Domain.Common;
using DddInPractice.Domain.SharedKernel;
using static DddInPractice.Domain.SharedKernel.Money;

namespace DddInPractice.Domain.BoundedContext.SnackMachines.SnackMachineAggregate
{
    public sealed class SnackMachine : AggregateRootBase
    {
        public const string SlotsNavigationPropertyName = nameof(Slots);

        private IList<Slot> Slots { get; }
        public Money MoneyInside { get; private set; }
        public decimal MoneyInTransaction { get; private set; }

        public SnackMachine()
        {
            MoneyInside = None;
            MoneyInTransaction = 0;
            Slots = new List<Slot>();
        }

        public SnackMachine(bool loadSlots) : this()
        {
            if (loadSlots)
                Slots = new List<Slot>()
                {
                    new Slot(this, 1),
                    new Slot(this, 2),
                    new Slot(this, 3),
                };
        }

        public SnackMachine(Money moneyInside, IList<Slot> slots)
        {
            MoneyInside = moneyInside;
            Slots = slots;
        }

        public Slot GetSlot(int position) => Slots.Single(s => s.Position == position);

        public SnackPile GetSnackPile(int position) => GetSlot(position).SnackPile;

        public IReadOnlyList<SnackPile> GetAllSnackPiles() => Slots
                .OrderBy(x => x.Position)
                .Select(x => x.SnackPile)
                .ToList();

        public void InsertMoney(Money money)
        {
            Money[] coinsAndNotes =
            {
                Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar
            };
            if (!coinsAndNotes.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money.Amount;
            MoneyInside += money;
        }

        public void ReturnMoney()
        {
            Money moneyToReturn = MoneyInside.Allocate(MoneyInTransaction);
            MoneyInside -= moneyToReturn;
            MoneyInTransaction = 0;
        }

        public string CanBuySnack(int position)
        {
            SnackPile snackPile = GetSnackPile(position);

            if (snackPile.Quantity == 0)
                return "The snack pile is empty";

            if (MoneyInTransaction < snackPile.Price)
                return "Not enough money";

            if (!MoneyInside.CanAllocate(MoneyInTransaction - snackPile.Price))
                return "Not enough change";

            return string.Empty;
        }

        public void BuySnack(int position)
        {
            var slot = GetSlot(position);

            if (slot.SnackPile.Price > MoneyInTransaction)
                throw new InvalidOperationException();

            slot.SnackPile = slot.SnackPile.SubtractOne();

            var moneyToAllocate = MoneyInTransaction - slot.SnackPile.Price;
            Money change = MoneyInside.Allocate(moneyToAllocate);
            if (change.Amount < moneyToAllocate)
                throw new InvalidOperationException();

            MoneyInside -= change;
            MoneyInTransaction = 0;
        }

        public void LoadSnacks(int position, SnackPile snackPile)
        {
            var slot = Slots.Single(s => s.Position == position);
            slot.SnackPile = snackPile;

        }

        public void LoadMoney(Money money)
        {
            MoneyInside += money;
        }

        public static SnackMachine CreateSnackMachineWithSlots()
        {
            var sm = new SnackMachine();

            sm.Slots.Add(new Slot(sm, 1));
            sm.Slots.Add(new Slot(sm, 2));
            sm.Slots.Add(new Slot(sm, 3));
            return sm;
        }
    }
}
