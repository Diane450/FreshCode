using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using System.Security.Principal;

namespace FreshCode.Mappers
{
    public static class UserFoodMapper
    {
        public static UserFoodDTO ToDTO(UserFood userFood)
        {
            return new UserFoodDTO
            {
                UserFoodId = userFood.Id,
                Food = FoodMapper.ToDTO(userFood.Food),
                Count = userFood.Count
            };
        }
    }
}
