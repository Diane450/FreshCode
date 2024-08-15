using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class CreatePetRepository : ICreatePetRepository
    {
        private readonly FreshCodeContext _dbContext;

        public CreatePetRepository(FreshCodeContext freshCodeContext)
        {
            _dbContext = freshCodeContext;
        }

        public async Task<List<EyeDTO>> GetEyesAsync()
        {
            return await _dbContext.Eyes
                .Select(eye => EyeMapper.ToDTO(eye))
                .ToListAsync();
        }

        public async Task<List<BodyDTO>> GetBodiesAsync()
        {
            return await _dbContext.Bodies
                .Select(body => BodyMapper.ToDTO(body))
                .ToListAsync();
        }
    }
}
