namespace DddInPractice.Domain
{
    public class Snack : AggregateRootBase
    {
        public string Name { get; private set; }
        public Snack(string name)
        {
            Name = name;
        }
    }
}
