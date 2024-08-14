using FreshCode.ModelsDTO;

namespace FreshCode.Requests
{
    public class SetArtifactRequest
    {
        public ArtifactDTO Artifact { get; set; } = null!;
        public PetDTO Pet { get; set; } = null!;
    }
}
