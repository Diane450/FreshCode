using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Repositories;
using FreshCode.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FreshCode.UseCases
{
    public class UserUseCase(IUserRepository userRepository,
        IClanRepository clanRepository,
        VkApiService vkApiService,
        IBaseRepository baseRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IClanRepository _clanRepository = clanRepository;
        private readonly VkApiService _vkApiService = vkApiService;
        private readonly IBaseRepository _baseRepository = baseRepository;

        public async Task<UserDTO> GetUserGameInfo(long userId)
        {
            return await _userRepository.GetUserGameInfo(userId);
        }

        public async Task<long> GetUserIdByVkId(long vk_user_id)
        {
            return await _userRepository.GetUserIdByVkId(vk_user_id);
        }

        public async Task<List<TaskDTO>> GetUserTasks(long userId)
        {
            var userTasks = _userRepository.GetUserTasks(userId);
            var userTaskDTOs = userTasks.Select(task => TaskMapper.ToDTO(task)).ToList();
            return userTaskDTOs;
        }

        public async Task<List<ArtifactHistoryDTO>> GetArtifactHistory(long userId, long bannerId)
        {
            return _userRepository.GetArtifactHistory(userId, bannerId)
                .Select(ah => ArtifactHistoryMapper.ToDTO(ah)).ToList();
        }

        public async Task<List<UserFoodDTO>> GetUserFood(long userId)
        {
            var food = _userRepository.GetUserFood(userId).ToList();

            return UserFoodMapper.ToDTO(food);
        }

        public async Task<List<ArtifactDTO>> GetUserArtifact(long userId)
        {
            return await _userRepository.GetUserArtifact(userId);
        }

        public async Task<List<BackgroundDTO>> GetUserBackgrounds(long userId)
        {
            return await _userRepository.GetUserBackgrounds(userId);
        }

        public async System.Threading.Tasks.Task SetBackground(long backgroundId, long userId)
        {
            User user = await _userRepository.GetUserById(userId);
            user.BackgroundId = backgroundId;
            await _baseRepository.SaveChangesAsync();
        }

        public async Task<List<UserRatingTableDTO>> GetAllUsersRatingTable()
        {
            List<UserRatingTableDTO> users = await _userRepository.GetAllUsersRatingTable();
            return [.. users.OrderByDescending(u => u.WonBattlesCount)];
        }

        public async Task<List<ClanRatingTableDTO>> GetClanRatingTable()
        {
            List<ClanRatingTableDTO> clans = await _clanRepository.GetClanRatingTable();
            return [.. clans.OrderByDescending(c => c.WonBattlesCount)];
        }

        public async Task<List<UserRatingTableDTO>> GetFriendsRatingTable(string vk_user_id)
        {
            List<long> friendsIds = await _vkApiService.GetUserFriendsIds(vk_user_id);

            return await _userRepository.GetFriendsRatingTable(friendsIds);
        }
    }
}
