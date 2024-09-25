using FreshCode.DbModels;
using FreshCode.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class BanerRepository : IBanerRepository
    {
        private readonly FreshCodeContext _dbContext;
        public BanerRepository(FreshCodeContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Banner> GetBannerById(long bannerId)
        {
            var banner = await _dbContext.Banners
                .Where(b => b.Id == bannerId)
                .Include(b => b.BannerItems)
                .ThenInclude(bi => bi.Artifact)
                .ThenInclude(a => a.Rarity)
                .Include(b => b.BannerItems)
                .ThenInclude(bi => bi.Artifact)
                .ThenInclude(bi => bi.ArtifactType)
                .Include(b => b.BannerItems)
                .ThenInclude(bi => bi.Artifact)
                .ThenInclude(bi => bi.ArtifactBonuses)
                .ThenInclude(bi => bi.Bonus)
                .ThenInclude(bi => bi.Type)
                .Include(b => b.BannerItems)
                .ThenInclude(bi => bi.Artifact)
                .ThenInclude(bi => bi.ArtifactBonuses)
                .ThenInclude(bi => bi.Bonus)
                .ThenInclude(bi => bi.Characteristic)
                .FirstOrDefaultAsync();
            if (banner == null)
            {
                throw new Exception($"Baner with id {bannerId} was not found");
            }
            return banner;
        }
    }
}
