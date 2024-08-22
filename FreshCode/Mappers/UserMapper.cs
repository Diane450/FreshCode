using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Mappers
{
    public class UserMapper
    {
        public static UserDTO ToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Money = user.Money,
                StatPoints = user.StatPoints,
                PrimogemsCount = user.PrimogemsCount,
                WonBattlesCount = user.WonBattlesCount,
                FatesCount = user.FatesCount,
                Background = BackgroundMapper.ToDTO(user.Background)
            };
        }

        public static UserRatingTableDTO ToRatingTableDTO(User user)
        {
            return new UserRatingTableDTO
            {
                Vk_user_id = user.VkId,
                WonBattlesCount = user.WonBattlesCount,
            };
        }
    }
}
