using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class ShopRepository(FreshCodeContext dbContext) : IShopRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public async Task<List<BackgroundDTO>> GetBackgroundsAsync()
        {
            return await _dbContext.Backgrounds
                .Select(bg=>BackgroundMapper.ToDTO(bg)).ToListAsync();
        }

        public async Task<List<ArtifactDTO>> GetArtifactsAsync()
        {
            return await _dbContext.ArtifactBonuses
                .Include(a=>a.Artifact)
                .ThenInclude(a=>a.Rarity)
                .Include(a => a.Artifact)
                .ThenInclude(a => a.ArtifatcType)
                .Include(a=>a.Bonus)
                .ThenInclude(b=>b.Characteristic)
                .Include(a => a.Bonus)
                .ThenInclude(b => b.Type)
                .Select (artifact => ArtifactMapper.ToArtifactDTO(artifact)).ToListAsync();
        }

        public async Task<List<FoodDTO>> GetFoodAsync() {
        
            return await _dbContext.FoodBonuses
                .Include(f => f.Food)
                .Include(f => f.Bonus)
                .ThenInclude(f => f.Characteristic)
                .Include(f => f.Bonus)
                .ThenInclude(f => f.Type)
                .Select(foodBonus => FoodMapper.ToDTO(foodBonus)).ToListAsync();
        }
    }
}
