using Microsoft.AspNetCore.Identity;

namespace FreshCode.Responses
{
    public class ArtifactResponse
    {
        public long ArtifactId { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public string Rarity { get; set; } = null!;

        public bool IsArtifactOwnedPreviously { get; set; }

        public int? Money { get; set; }
    }
}
