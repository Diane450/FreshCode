namespace FreshCode.ModelsDTO
{
    public class ArtifactHistoryDTO
    {
        public long ArtifactHistoryId { get; set; }

        public ArtifactDTO Artifact { get; set; } = null!;

        public int GotAt { get; set; }
    }
}
