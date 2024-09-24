using FreshCode.DbModels;

namespace FreshCode.Interfaces
{
    public interface IArtifactRepository
    {
        public Task<Artifact> GetArtifactById(long artifactId);
    }
}
