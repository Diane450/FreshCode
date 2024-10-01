namespace FreshCode.ModelsDTO
{
    public class ArtifactHistoryDTO
    {
        public long Id { get; set; }

        public ArtifactDTO Artifact { get; set; } = null!;

        public DateTime? GotAt { get; set; }
    }
}
