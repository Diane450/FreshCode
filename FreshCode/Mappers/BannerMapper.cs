using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Mappers
{
    public static class BannerMapper
    {
        public static BanerDTO ToDTO(Banner banner)
        {
            return new BanerDTO()
            {
                Id = banner.Id,
                CreatedAt = banner.CreatedAt,
                ExpiresAt = banner.ExpiresAt,
                Artifacts = banner.BannerItems.Select(bi => ArtifactMapper.ToDTO(bi.Artifact)).ToList()
            };
        }
    }
}
