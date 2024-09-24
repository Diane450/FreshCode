using FreshCode.ModelsDTO;

namespace FreshCode.Requests
{
    public class RemoveArtifactRequest
    {
        public long ArtifactToRemoveId { get; set; }
        public long PetId { get; set; }
    }
}
