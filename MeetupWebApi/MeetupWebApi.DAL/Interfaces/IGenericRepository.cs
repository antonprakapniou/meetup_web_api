using System.Linq.Expressions;

namespace MeetupWebApi.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public Task AddAsync(T entity);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task<IEnumerable<T>> GetByAsync(Expression<Func<T, bool>> expression);
        public void Update(T entity);
        public void Delete(T entity);
        public Task SaveChangesAsync();
    }
}