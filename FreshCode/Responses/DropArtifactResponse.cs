namespace FreshCode.Responses
{
    public class DropArtifactResponse
    {
        public List<ArtifactResponse> artifacts { get; set; }

        public int TotalMoney { get; set; }

        public int TotalFates { get; set; }
    }
}
