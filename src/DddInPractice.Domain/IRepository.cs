using System.Threading.Tasks;

namespace DddInPractice.Domain
{
    public interface IRepository<T> where T : AggregateRootBase
    {
        public T? GetById(long Id);

        public void Save(T entity);
    }
}
