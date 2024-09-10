using FreshCode.ModelsDTO;

namespace FreshCode.Requests
{
    public class RemoveArtifactRequest
    {
        public ArtifactDTO ArtifactToRemove { get; set; } = null!;
        public long PetId { get; set; }
    }
}
