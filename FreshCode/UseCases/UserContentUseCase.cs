using FreshCode.Interfaces;
using FreshCode.ModelsDTO;

namespace FreshCode.UseCases
{
    public class UserContentUseCase
    {
        private readonly IUserRepository _userRepository;
        
        public UserContentUseCase(IUserRepository userRepository)
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

        public async Task<List<TaskDTO>> GetUserTasks(string vk_user_id)
        {
            return await _userRepository.GetUserTasks(vk_user_id);
        }

        public async Task<List<ArtifactHistoryDTO>> GetArtifactHistory(string vk_user_id)
        {
            return await _userRepository.GetArtifactHistory(vk_user_id);
        }

        public async Task<List<UserFoodDTO>> GetUserFood(string vk_user_id)
        {
            return await _userRepository.GetUserFood(vk_user_id);
        }

        public async Task<List<ArtifactDTO>> GetUserArtifact(string vk_user_id)
        {
            return await _userRepository.GetUserArtifact(vk_user_id);
        }

        public async Task<List<BackgroundDTO>> GetUserBackgrounds(string vk_user_id)
        {
            return await _userRepository.GetUserBackgrounds(vk_user_id);
        }
    }
}
