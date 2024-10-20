using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Interfaces
{
    public interface IBanerRepository
    {
        Task<BanerDTO> GetBannerInfo(long bannerId);
        
        Task<Banner> GetBannerById(long bannerId);

        IQueryable<BannerItem> GetArtifactsByBanner(long bannerId);
        
        IQueryable<BannerItem> GetAllBannerItems();
    }
}
