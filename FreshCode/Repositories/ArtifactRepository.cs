﻿using FreshCode.DbModels;
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
                .Include(a=>a.ArtifactType)
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
    }
}
