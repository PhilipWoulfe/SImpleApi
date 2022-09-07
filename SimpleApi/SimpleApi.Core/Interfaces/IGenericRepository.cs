using SimpleApi.Core.ProjectAggregate;
using System.Linq.Expressions;

namespace SimpleApi.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        T Add(T entity);
    }
}
