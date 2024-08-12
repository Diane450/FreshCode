using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Mappers
{
    public static class FoodMapper
    {
        public static FoodDTO ToDTO(FoodBonuse food)
        {
            return new FoodDTO
            {
                Id = food.Id,
                X = food.Food.X,
                Y = food.Food.Y,
                Name = food.Food.Name,
                Price = food.Food.Price,
                Bonus = $"{food.Bonus.Characteristic.Characteristic1} +{food.Bonus.Value}{(food.Bonus.Type.Type == "percent" ? '%' : string.Empty)}",
                Characteristic = food.Bonus.Characteristic.Characteristic1,
                Value = food.Bonus.Value,
                Type = food.Bonus.Type.Type
            };
        }
    }
}
