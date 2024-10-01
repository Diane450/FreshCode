namespace FreshCode.Responses
{
    public class GetArtifactResponse
    {
        public long ArtifactId { get; set; }

        public bool IsUserHasArtifact { get; set; }

        public int? Money { get; set; }
    }
}
