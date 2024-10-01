using FreshCode.DbModels;

namespace FreshCode.Interfaces
{
    public interface IBanerRepository
    {
        Task<Banner> GetBannerById(long bannerId);

        IQueryable<BannerItem> GetArtifactsByBanner(long bannerId);
    }
}
