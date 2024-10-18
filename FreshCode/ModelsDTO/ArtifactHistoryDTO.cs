namespace FreshCode.ModelsDTO
{
    /// <summary>
    /// DTO история круток в баннере
    /// </summary>
    public class ArtifactHistoryDTO
    {
        /// <summary>
        /// Id записи в таблице
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// DTO артефакта
        /// </summary>
        public ArtifactDTO Artifact { get; set; } = null!;
        /// <summary>
        /// когда был получен
        /// </summary>
        public DateTime? GotAt { get; set; }
    }
}
