﻿
using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Dapper_Interfaces
{
    public interface IUserRepositoryDapper
    {
        Task <UserDTO> GetUserGameInfo(long userId);
        Task<List<TaskDTO>> GetUserTasks(long vk_user_id);
        Task<List<ArtifactHistoryDTO>> GetArtifactHistory(long vk_user_id);
        Task<List<UserFoodDTO>> GetUserFood(long userId);
        Task<List<ArtifactDTO>> GetUserArtifacts(long userId);
        Task<List<BackgroundDTO>> GetUserBackgrounds(long userId);
        Task<long> GetUserIdByVkId(string vk_user_id);
        Task<User> GetUserByVkId(string vk_user_id);
        System.Threading.Tasks.Task CreateNewClan(Clan clan);
        System.Threading.Tasks.Task AddUserClan(UserClan userClan);
        Task<Clan> GetClanByUser(long userId);
        Task<List<UserRatingTableDTO>> GetAllUsersRatingTable();
        Task<List<UserRatingTableDTO>> GetFriendsRatingTable(List<long> friendsIds);
    }
}
