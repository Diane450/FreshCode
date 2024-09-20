using FreshCode.DbModels;
using FreshCode.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class BaseRepository(FreshCodeContext dbContext) : IBaseRepository
    {
        private FreshCodeContext _dbContext = dbContext;

        public void Remove<T>(T entity) where T : class
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async System.Threading.Tasks.Task AddAsync<T>(T entity) where T : class
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public async System.Threading.Tasks.Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Update<T>(T entity) where T : class
        {
             _dbContext.Set<T>().Update(entity);
        }

        public void RemoveRange<T>(List<T> entities) where T : class
        {
            _dbContext.RemoveRange(entities);
        }
    }
}
