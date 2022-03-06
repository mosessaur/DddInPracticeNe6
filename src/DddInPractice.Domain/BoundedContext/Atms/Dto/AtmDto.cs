namespace DddInPractice.Domain.BoundedContext.Atms.Dto
{
    public class AtmDto
    {
        public long Id { get; }
        public decimal Cash { get; }

        public AtmDto(long id, decimal cash)
        {
            Id = id;
            Cash = cash;
        }
    }
}
