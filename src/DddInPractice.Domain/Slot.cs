namespace DddInPractice.Domain
{
    public class Slot : EntityBase
    {
        public int Position { get; private set; }
        public SnackPile SnackPile { get; internal set; }
        public SnackMachine SnackMachine { get; internal set; }

        public Slot(SnackMachine snackMachine, int position)
        {
            SnackMachine = snackMachine;
            Position = position;
            SnackPile = new SnackPile(null, 0, 0m);
        }
    }
}
