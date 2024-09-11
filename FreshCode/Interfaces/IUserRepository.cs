
using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Interfaces
{
    public interface IUserRepository
    {
        Task <UserDTO> GetUserGameInfo(long userId);
        Task<List<TaskDTO>> GetUserTasks(long userId);
        Task<List<ArtifactHistoryDTO>> GetArtifactHistory(long userId);
        Task<List<UserFoodDTO>> GetUserFood(long userId);
        Task<List<ArtifactDTO>> GetUserArtifact(long userId);
        Task<List<BackgroundDTO>> GetUserBackgrounds(long userId);
        Task<long> GetUserIdByVkId(string vk_user_id);
        Task<User> GetUserByVkId(string vk_user_id);
        System.Threading.Tasks.Task CreateNewClan(Clan clan);
        System.Threading.Tasks.Task AddUserClan(UserClan userClan);
        Task<Clan> GetClanByUser(long userId);
        Task<List<UserRatingTableDTO>> GetAllUsersRatingTable();
        Task<List<UserRatingTableDTO>> GetFriendsRatingTable(List<long> friendsIds);
        Task<UserFood> GetUserFoodByFoodId(long id);
        public Task<bool> isArtifactAbsent(long artifactId, long userId);
    }
}
