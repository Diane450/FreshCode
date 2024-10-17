using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class EyesRepository : IEyesRepository
    {
        private readonly FreshCodeContext _dbContext;

        public EyesRepository(FreshCodeContext freshCodeContext)
        {
            _dbContext = freshCodeContext;
        }

        public IQueryable<Eye> GetEyesAsync()
        {
            return _dbContext.Eyes;
        }
    }
}
