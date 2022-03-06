namespace DddInPractice.Domain.BoundedContext.SnackMachines.Dto
{
    public class SnackMachineDto
    {
        public long Id { get; }
        public decimal MoneyInside { get; }

        public SnackMachineDto(long id, decimal moneyInside)
        {
            Id = id;
            MoneyInside = moneyInside;
        }
    }
}
