using FreshCode.ModelsDTO;

namespace FreshCode.Requests
{
    /// <summary>
    /// Запрос на снятие артефакта
    /// </summary>
    public class RemoveArtifactRequest
    {
        /// <summary>
        /// Id артефакта
        /// </summary>
        public long ArtifactToRemoveId { get; set; }
        /// <summary>
        /// Id питомца
        /// </summary>
        public long PetId { get; set; }
    }
}
