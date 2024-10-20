using Microsoft.AspNetCore.Identity;

namespace FreshCode.ModelsDTO
{
    public class BanerDTO
    {/// <summary>
    /// Id баннера
    /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Когда баннер создан
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// до какого действует 
        /// </summary>
        public DateTime ExpiresAt { get; set; }
        /// <summary>
        /// какие артефакты могут выпасть 
        /// </summary>
        public List<ArtifactDTO> Artifacts { get; set; } = null!;
    }
}