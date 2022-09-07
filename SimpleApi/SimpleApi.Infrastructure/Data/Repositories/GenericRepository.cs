using SimpleApi.Core.Interfaces;
using System.Linq.Expressions;

namespace SimpleApi.Infrastructure.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _dbContext;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Add(T entity)
        {
            var savedEntity = _dbContext.Set<T>().Add(entity).Entity;
            _dbContext.SaveChanges();
            return savedEntity;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T GetById(Guid id)
        {
            return _dbContext.Set<T>().Find(id);
        }
    }
}
