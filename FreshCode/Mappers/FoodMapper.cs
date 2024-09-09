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
                Bonuses = food.FoodBonuses.Select(fb=> BonusMapper.ToDTO(fb.Bonus)).ToList(),
            };
        }
    }
}
