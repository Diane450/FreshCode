﻿using FreshCode.DbModels;
using FreshCode.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class FoodRepository(FreshCodeContext dbContext) : IFoodRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public async Task<Food> GetFoodById(long foodId)
        {
            var food = await _dbContext.Foods
                .Include(f => f.FoodBonuses)
                .FirstOrDefaultAsync(f => f.Id == foodId);

            if (food == null)
                throw new Exception($"Food with id {foodId} not found");

            return food;
        }

        public async Task<int> GetFoodPrice(long foodId)
        {
            var price = await _dbContext.Foods
                .Where(f => f.Id == foodId)
                .Select(f => f.Price).FirstOrDefaultAsync();

            if (price == null || price == 0)
            {
                throw new ArgumentException("Еда не была найдена");
            }
            return price;
        }
    }
}
