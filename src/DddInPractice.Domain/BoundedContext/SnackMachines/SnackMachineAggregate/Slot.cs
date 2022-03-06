using DddInPractice.Domain.Common;

namespace DddInPractice.Domain.BoundedContext.SnackMachines.SnackMachineAggregate
{
    public class Slot : EntityBase
    {
        public int Position { get; private set; }
        public SnackPile SnackPile { get; internal set; }
        public SnackMachine? SnackMachine { get; internal set; }

        private Slot() //Required by EF
        {
            SnackPile = SnackPile.Empty;
        }

        public Slot(SnackMachine snackMachine, int position, SnackPile snackPile)
        {
            SnackMachine = snackMachine;
            Position = position;
            SnackPile = snackPile;
        }

        public Slot(SnackMachine snackMachine, int position)
            : this(snackMachine, position, SnackPile.Empty)
        {
        }
    }
}
