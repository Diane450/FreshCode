using FreshCode.ModelsDTO;

namespace FreshCode.Requests
{
    public class SetArtifactRequest
    {
        public ArtifactDTO Artifact { get; set; } = null!;
        public long PetId { get; set; }
    }
}
