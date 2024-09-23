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
                Id = userFood.Id,
                Food = FoodMapper.ToDTO(userFood.Food),
                Count = userFood.Count
            };
        }

        public static List<UserFoodDTO> ToDTO(List<UserFood> userFoods)
        {
            List<UserFoodDTO> userFoodDTO = new List<UserFoodDTO>();
            foreach (var userFood in userFoods)
            {
                userFoodDTO.Add(ToDTO(userFood));
            }
            return userFoodDTO;
        }
    }
}
