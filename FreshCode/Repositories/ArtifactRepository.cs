using FreshCode.DbModels;
using FreshCode.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class ArtifactRepository(FreshCodeContext dbContext) : IArtifactRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public async Task<Artifact> GetArtifactById(long artifactId)
        {
            var artifact = await _dbContext.Artifacts
                .Include(a => a.Rarity)
                .Include(a => a.ArtifactType)
                .Include(a => a.ArtifactBonuses)
                .ThenInclude(ab => ab.Bonus)
                .ThenInclude(b => b.Characteristic)
                .Include(a => a.ArtifactBonuses)
                .ThenInclude(ab => ab.Bonus)
                .ThenInclude(b => b.Type)
                .FirstOrDefaultAsync(a => a.Id == artifactId);

            if (artifact == null)
            {
                throw new Exception($"Artifact with Id {artifactId} not found");
            }
            return artifact;
        }

        public async Task<List<Artifact?>> GetPetArtifacts(long petId)
        {
            return _dbContext.Pets
                .Where(p => p.Id == petId)
                .Include(p => p.Accessory)
                .ThenInclude(a => a.ArtifactType)
                .Include(p => p.Accessory)
                .ThenInclude(a => a.Rarity)
                .Include(a => a.Accessory)
                .ThenInclude(a => a.ArtifactBonuses)
                .ThenInclude(ab => ab.Bonus)
                .ThenInclude(b => b.Characteristic)
                .ThenInclude(ab => ab.Bonus)
                .ThenInclude(b => b.Type)
                .Include(a => a.Hat)
                .ThenInclude(a => a.ArtifactType)

                .Include(p => p.Hat)
                .ThenInclude(a => a.ArtifactType)
                .Include(p => p.Hat)
                .ThenInclude(a => a.Rarity)
                .Include(a => a.Hat)
                .ThenInclude(a => a.ArtifactBonuses)
                .ThenInclude(ab => ab.Bonus)
                .ThenInclude(b => b.Characteristic)
                .ThenInclude(ab => ab.Bonus)
                .ThenInclude(b => b.Type)
                .Include(a => a.Hat)
                .ThenInclude(a => a.ArtifactType)
                .AsEnumerable()
                .SelectMany(p => new[] { p.Accessory, p.Hat }) // Извлекаем оба артефакта
                .ToList();
        }
    }
}
