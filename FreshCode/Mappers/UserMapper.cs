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
                FatesCount = user.FatesCount,
                Background = BackgroundMapper.ToDTO(user.Background)
            };
        }
    }
}
