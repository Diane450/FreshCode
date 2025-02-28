﻿using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class ShopRepository(FreshCodeContext dbContext) : IShopRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public async Task<List<BackgroundDTO>> GetBackgroundsAsync()
        {
            return await _dbContext.Backgrounds
                .Select(bg=>BackgroundMapper.ToDTO(bg)).ToListAsync();
        }

        public async Task<List<ArtifactDTO>> GetArtifactsAsync()
        {
            return await _dbContext.Artifacts
                .Include(a=>a.Rarity)
                .Include(a=>a.ArtifactType)
                .Include(a=>a.ArtifactBonuses)
                .ThenInclude(ab=>ab.Bonus)
                .ThenInclude(b=>b.Characteristic)
                .Include(a => a.ArtifactBonuses)
                .ThenInclude(ab => ab.Bonus)
                .ThenInclude(b => b.Type)
                .Select(a=>ArtifactMapper.ToDTO(a))
                .ToListAsync();
        }

        public async Task<List<FoodDTO>> GetFoodAsync()
        {
            return await _dbContext.Foods
                .Include(f=>f.FoodBonuses)
                .ThenInclude(fb=>fb.Bonus)
                .ThenInclude(b => b.Characteristic)
                .Include(f => f.FoodBonuses)
                .ThenInclude(fb => fb.Bonus)
                .ThenInclude(b => b.Type)
                .Select(f=>FoodMapper.ToDTO(f))
                .ToListAsync();
        }
    }
}
