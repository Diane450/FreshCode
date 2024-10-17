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

        public IQueryable<Body> GetBodiesAsync()
        {
            return _dbContext.Bodies;
        }
    }
}
