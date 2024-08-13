using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Mappers
{
    public static class FoodMapper
    {
        public static FoodDTO ToDTO(Food food)
        {
            return new FoodDTO
            {
                Id = food.Id,
                X = food.X,
                Y = food.Y,
                Name = food.Name,
                Price = food.Price,
                Bonus = food.FoodBonuses.Select(fb=> BonusMapper.ToDTO(fb.Bonus)).ToList()
                //Bonus = $"{food.Characteristic.Characteristic1} +{food.Bonus.Value}{(food.Bonus.Type.Type == "percent" ? '%' : string.Empty)}",
                //Characteristic = food.Bonus.Characteristic.Characteristic1,
                //Value = food.Bonus.Value,
                //Type = food.Bonus.Type.Type
            };
        }
    }
}
