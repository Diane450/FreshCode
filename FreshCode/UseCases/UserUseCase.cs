﻿using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.ModelsDTO;
using FreshCode.Services;

namespace FreshCode.UseCases
{
    public class UserUseCase(IUserRepository userRepository, IClanRepository clanRepository, VkApiService vkApiService)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IClanRepository _clanRepository = clanRepository;
        private readonly VkApiService _vkApiService = vkApiService;

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

        public async System.Threading.Tasks.Task SetBackground(long backgroundId, string vk_user_id)
        {
            User user = await _userRepository.GetUserByVkId(vk_user_id);
            user.BackgroundId = backgroundId;
            await _userRepository.SaveChangesAsync();
        }

        public async Task<List<UserRatingTableDTO>> GetAllUsersRatingTable()
        {
            List<UserRatingTableDTO> users = await _userRepository.GetAllUsersRatingTable();
            return users.OrderByDescending(u => u.WonBattlesCount).ToList();
        }

        public async Task<List<ClanRatingTableDTO>> GetClanRatingTable()
        {
            List<ClanRatingTableDTO> clans = await _clanRepository.GetClanRatingTable();
            return clans.OrderByDescending(c => c.WonBattlesCount).ToList();
        }

        public async Task<List<UserRatingTableDTO>> GetFriendsRatingTable(string vk_user_id)
        {
            List<long> friendsIds = await _vkApiService.GetUserFriendsIds(vk_user_id);

            return await _userRepository.GetFriendsRatingTable(friendsIds);
        }
    }
}
