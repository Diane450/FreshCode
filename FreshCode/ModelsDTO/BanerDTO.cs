using Microsoft.AspNetCore.Identity;

namespace FreshCode.ModelsDTO
{
    public class BanerDTO
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public List<ArtifactDTO> Artifacts { get; set; } = null!;
    }
}