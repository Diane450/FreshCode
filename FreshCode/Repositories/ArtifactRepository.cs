using FreshCode.DbModels;
using FreshCode.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class ArtifactRepository : IArtifactRepository
    {
        private readonly FreshCodeContext _dbContext;

        public ArtifactRepository(FreshCodeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetArtifactPriceById(long artifactId)
        {
            return await _dbContext.Artifacts.Where(a => a.Id == artifactId)
                .Select(a => a.Price).FirstOrDefaultAsync();
        }
    }
}
