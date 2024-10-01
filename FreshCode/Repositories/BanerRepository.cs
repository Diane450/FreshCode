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

        public IQueryable<BannerItem> GetArtifactsByBanner(long bannerId)
        {
            return _dbContext.BannerItems
                .Where(bi => bi.BannerId == bannerId);
        }

        public async Task<Banner> GetBannerById(long bannerId)
        {
            var banner = await _dbContext.Banners
                .Where(b => b.Id == bannerId)
                .Include(b => b.BannerItems)
                .FirstOrDefaultAsync();
            if (banner == null)
            {
                throw new Exception($"Baner with id {bannerId} was not found");
            }
            return banner;
        }
    }
}
