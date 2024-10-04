namespace FreshCode.Responses
{
    public class DropArtifactResponse
    {
        public long ArtifactId { get; set; }

        public bool IsArtifactOwnedPreviously { get; set; }

        public int? Money { get; set; }
    }
}
