namespace DddInPractice.Domain.Common
{
    public interface IRepository<T> where T : AggregateRootBase
    {
        public T? GetById(long Id);

        public void Save(T entity);
    }
}
