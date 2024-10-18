
using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using Task = FreshCode.DbModels.Task;

namespace FreshCode.Interfaces
{
    public interface IUserRepository
    {
        Task <UserDTO> GetUserGameInfo(long userId);
        IQueryable <UserTask> GetUserTasks(long userId);
        IQueryable <ArtifactHistory> GetArtifactHistory(long userId, long bannerId);
        IQueryable<UserFood> GetUserFood(long userId);
        Task<List<ArtifactDTO>> GetUserArtifact(long userId);
        IQueryable<UserBackground> GetUserBackgrounds(long userId);
        Task<long> GetUserIdByVkId(long vk_user_id);
        Task<User> GetUserByVkId(long vk_user_id);
        System.Threading.Tasks.Task CreateNewClan(Clan clan);
        System.Threading.Tasks.Task AddUserClan(UserClan userClan);
        Task<Clan> GetClanByUser(long userId);
        IQueryable<User> GetAllUsers();
        Task<List<UserRatingTableDTO>> GetFriendsRatingTable(List<long> friendsIds);
        Task<UserFood> GetUserFoodByFoodId(long id);
        public Task<bool> isArtifactAbsent(long artifactId, long userId);
        public Task<bool> isBackgroundAbsent(long backgroundId, long userId);
        public Task<User> GetUserById(long userId);
        IQueryable<User> GetUsersByClanId(long clanId);
        IQueryable<User> GetClanUsers(long userId);
    }
}
