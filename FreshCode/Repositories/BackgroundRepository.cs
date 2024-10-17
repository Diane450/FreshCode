using FreshCode.DbModels;
using FreshCode.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FreshCode.Repositories
{
    public class BackgroundRepository : IBackgroundRepository
    {
        private readonly FreshCodeContext _dbContext;
        public BackgroundRepository(FreshCodeContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Background> GetBackgroundById(long id)
        {
            var background = await _dbContext.Backgrounds.FindAsync(id);

            if (background == null)
            {
                throw new KeyNotFoundException($"Фон с ID {id} не найден.");
            }
            return background;
        }
    }
}
