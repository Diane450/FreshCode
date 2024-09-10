using FreshCode.DbModels;

namespace FreshCode.Interfaces
{
    public interface IArtifactRepository
    {
        public Task<int> GetArtifactPriceById(long artifactId);
    }
}
