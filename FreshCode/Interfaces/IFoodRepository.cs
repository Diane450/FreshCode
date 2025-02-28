﻿using FreshCode.DbModels;

namespace FreshCode.Interfaces
{
    public interface IFoodRepository
    {
        public Task<Food> GetFoodById(long foodId);
        Task<int> GetFoodPrice(long foodId);
    }
}
