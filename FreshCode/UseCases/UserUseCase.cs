using FreshCode.Interfaces;
using FreshCode.ModelsDTO;

namespace FreshCode.UseCases
{
    public class UserUseCase
    {
        private readonly IUserRepository _userRepository;
        
        public UserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task InventoryDecreaseFoodCountAsync(string vk_user_id, FoodDTO food)
        {
            await _userRepository.InventoryDecreaseFoodCount(vk_user_id, food);
        }

        public async Task<UserDTO> GetUserGameInfo(string vk_user_id)
        {
            return await _userRepository.GetUserGameInfo(vk_user_id);
        }
    }
}
