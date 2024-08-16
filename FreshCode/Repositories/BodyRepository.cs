using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class BodyRepository : IBodyRepository
    {
        private readonly FreshCodeContext _dbContext;

        public BodyRepository(FreshCodeContext freshCodeContext)
        {
            _dbContext = freshCodeContext;
        }

        public async Task<List<BodyDTO>> GetBodiesAsync()
        {
            return await _dbContext.Bodies
                .Select(body => BodyMapper.ToDTO(body))
                .ToListAsync();
        }

        public async Task<Body> GetBodyById(long id)
        {
            return await _dbContext.Bodies.FirstAsync(b => b.Id == id);
        }
    }
}
