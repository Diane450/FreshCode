using FreshCode.DbModels;
using FreshCode.EF_Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.EF_Repositories
{
    public class EyesRepository : IEyesRepository
    {
        private readonly FreshCodeContext _dbContext;

        public EyesRepository(FreshCodeContext freshCodeContext)
        {
            _dbContext = freshCodeContext;
        }

        public async Task<List<EyeDTO>> GetEyesAsync()
        {
            return await _dbContext.Eyes
                .Select(eye => EyeMapper.ToDTO(eye))
                .ToListAsync();
        }

        public async Task<Eye> GetEyesById(long id)
        {
            return await _dbContext.Eyes.FirstAsync(e => e.Id == id);
        }
    }
}
