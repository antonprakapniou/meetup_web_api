using MeetupWebApi.DAL.EF;
using MeetupWebApi.DAL.Interfaces;
using MeetupWebApi.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MeetupWebApi.DAL.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        internal DbSet<T> dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context= context;
            dbSet=_context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await dbSet.AsNoTracking().ToListAsync();

        public async Task<IEnumerable<T>> GetByAsync(Expression<Func<T, bool>> expression) =>
            await dbSet.AsNoTracking().Where(expression).ToListAsync();

        public void Update(T entity)
        {
            dbSet.Entry(entity).State= EntityState.Modified;
        }

        public void Delete(T entity)
        {
            dbSet.Entry(entity).State= EntityState.Deleted;
        }        

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }        
    }
}