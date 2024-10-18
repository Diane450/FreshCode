using FreshCode.ModelsDTO;

namespace FreshCode.Requests
{
    /// <summary>
    /// Запрос на установить новый артефакт
    /// </summary>
    public class SetArtifactRequest
    {
        /// <summary>
        /// Id артефакта
        /// </summary>
        public long ArtifactId { get; set; }
        /// <summary>
        /// Id питомца
        /// </summary>
        public long PetId { get; set; }
    }
}
