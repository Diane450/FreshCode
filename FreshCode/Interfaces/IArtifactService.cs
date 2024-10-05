using FreshCode.DbModels;

namespace FreshCode.Interfaces
{
    public interface IArtifactService
    {
        public void AssignArtifact(Pet pet, Artifact artifact);

        public void RemoveArtifact(Pet pet, Artifact artifact);
    }
}
