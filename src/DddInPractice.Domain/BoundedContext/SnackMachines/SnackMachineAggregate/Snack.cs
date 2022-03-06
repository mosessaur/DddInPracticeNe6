using DddInPractice.Domain.Common;

namespace DddInPractice.Domain.BoundedContext.SnackMachines.SnackMachineAggregate
{
    public class Snack : AggregateRootBase
    {
        public static Snack None => new Snack(0, "None");
        public static Snack Chocolate => new Snack(1, "Chocolate");
        public static Snack Soda => new Snack(2, "Soda");
        public static Snack Gum => new Snack(3, "Gum");

        public string Name { get; private set; }
        public Snack(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
