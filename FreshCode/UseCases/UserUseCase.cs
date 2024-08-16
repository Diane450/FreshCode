using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.ModelsDTO;

namespace FreshCode.UseCases
{
    public class UserUseCase(IUserRepository userRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<UserDTO> GetUserGameInfo(string vk_user_id)
        {
            long userId = await _userRepository.GetUserIdByVkId(vk_user_id);
            return await _userRepository.GetUserGameInfo(userId);
        }

        public async Task<List<TaskDTO>> GetUserTasks(string vk_user_id)
        {
            long userId = await _userRepository.GetUserIdByVkId(vk_user_id);
            return await _userRepository.GetUserTasks(userId);
        }

        public async Task<List<ArtifactHistoryDTO>> GetArtifactHistory(string vk_user_id)
        {
            long userId = await _userRepository.GetUserIdByVkId(vk_user_id);
            return await _userRepository.GetArtifactHistory(userId);
        }

        public async Task<List<UserFoodDTO>> GetUserFood(string vk_user_id)
        {
            long userId = await _userRepository.GetUserIdByVkId(vk_user_id);
            return await _userRepository.GetUserFood(userId);
        }

        public async Task<List<ArtifactDTO>> GetUserArtifact(string vk_user_id)
        {
            long userId = await _userRepository.GetUserIdByVkId(vk_user_id);
            return await _userRepository.GetUserArtifact(userId);
        }

        public async Task<List<BackgroundDTO>> GetUserBackgrounds(string vk_user_id)
        {
            long userId = await _userRepository.GetUserIdByVkId(vk_user_id);
            return await _userRepository.GetUserBackgrounds(userId);
        }
    }
}
