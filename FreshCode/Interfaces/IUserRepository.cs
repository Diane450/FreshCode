
using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Interfaces
{
    public interface IUserRepository
    {
        Task <UserDTO> GetUserGameInfo(long userId);
        Task<List<TaskDTO>> GetUserTasks(long userId);
        IQueryable <ArtifactHistory> GetArtifactHistory(long userId, long bannerId);
        IQueryable<UserFood> GetUserFood(long userId);
        Task<List<ArtifactDTO>> GetUserArtifact(long userId);
        Task<List<BackgroundDTO>> GetUserBackgrounds(long userId);
        Task<long> GetUserIdByVkId(long vk_user_id);
        Task<User> GetUserByVkId(long vk_user_id);
        System.Threading.Tasks.Task CreateNewClan(Clan clan);
        System.Threading.Tasks.Task AddUserClan(UserClan userClan);
        Task<Clan> GetClanByUser(long userId);
        Task<List<UserRatingTableDTO>> GetAllUsersRatingTable();
        Task<List<UserRatingTableDTO>> GetFriendsRatingTable(List<long> friendsIds);
        Task<UserFood> GetUserFoodByFoodId(long id);
        public Task<bool> isArtifactAbsent(long artifactId, long userId);
        public Task<bool> isBackgroundAbsent(long backgroundId, long userId);
        public Task<User> GetUserById(long userId);
        IQueryable<User> GetUsersByClanId(long clanId);
    }
}
