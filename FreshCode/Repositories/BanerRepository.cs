using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.ModelsDTO;
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

        public IQueryable<BannerItem> GetAllBannerItems()
        {
            return _dbContext.BannerItems
                .Include(bi => bi.Artifact)
                .ThenInclude(a => a.Rarity);
        }

        public IQueryable<BannerItem> GetArtifactsByBanner(long bannerId)
        {
            return _dbContext.BannerItems
                .Where(bi => bi.BannerId == bannerId);
        }

        public async Task<Banner> GetBannerById(long bannerId)
        {
            return await _dbContext.Banners
                .FindAsync(bannerId);
        }

        public async Task<BanerDTO> GetBannerInfo(long bannerId)
        {
            var bannerDto = await _dbContext.Banners
                .Where(b => b.Id == bannerId)
                .Select(b => new BanerDTO
                {
                    Id = b.Id,
                    CreatedAt = b.CreatedAt,
                    ExpiresAt = b.ExpiresAt,
                    Artifacts = b.BannerItems
                        .Select(bi => new ArtifactDTO
                        {
                            Id = bi.Artifact.Id,
                            X = bi.Artifact.X,
                            Y = bi.Artifact.Y
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (bannerDto == null)
            {
                throw new Exception($"Banner with id {bannerId} was not found");
            }

            return bannerDto;
        }
    }
}
