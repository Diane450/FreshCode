using Dapper;
using FreshCode.Dapper_Interfaces;
using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using Npgsql;
using System.Diagnostics;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FreshCode.Dapper_Repositories
{
    public class UserRepository : IUserRepositoryDapper
    {
        private readonly string _connectionString = null!;
        private readonly ISqlConnectionFactory _connectionFactory;

        public UserRepository(IConfiguration configuration, ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<TaskDTO>> GetUserTasks(long vk_user_id)
        {
            using var connection = _connectionFactory.Create();
            connection.Open();

            var sql = """
                      SELECT "Tasks"."Id" AS "Id", "Descryption", "MoneyReward", "PointsReward", "StatPointsReward", "PrimogemsReward", "IsCompleted"
                      FROM "User_Tasks"
                      JOIN "Tasks" ON "User_Tasks"."Task_Id" = "Tasks"."Id"
                      JOIN "User" ON "User_Tasks"."User_Id" = "User"."Id"
                      WHERE "Vk_Id" = @vkUserId
                      """;
            var parameters = new { vkUserId = vk_user_id };

            var tasks = await connection.QueryAsync<TaskDTO>(sql, parameters);

            return tasks.AsList();
        }

        public async Task<UserDTO> GetUserGameInfo(long vk_user_id)
        {
            using var connection = _connectionFactory.Create();
            connection.Open();

            var sql = """
                SELECT "User"."Id", "Money", "StatPoints", "WonBattles_Count" AS "WonBattlesCount",
                "Vk_Id", "Primogems_Count" AS "PrimogemsCount", "Fates_Count" AS "FatesCount", "Background"."Id" AS "BackgroundId",
                "X", "Y", "Price"
                FROM "User"
                JOIN "Background" ON "User"."Background_Id" = "Background"."Id"
                Where "User"."Vk_Id" = @vkId
                """
            ;

            var user = await connection.QueryAsync<UserDTO, BackgroundDTO, UserDTO>(
                sql,
                (userDto, backgroundDto) =>
                {
                    userDto.Background = backgroundDto;
                    return userDto;
                },
                splitOn: "BackgroundId",
                param: new { vkId = vk_user_id }
            );
            return user.First();
        }

        public System.Threading.Tasks.Task AddUserClan(UserClan userClan)
        {
            throw new NotImplementedException();
        }

        public System.Threading.Tasks.Task CreateNewClan(Clan clan)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserRatingTableDTO>> GetAllUsersRatingTable()
        {
            throw new NotImplementedException();
        }

        public Task<List<ArtifactHistoryDTO>> GetArtifactHistory(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<Clan> GetClanByUser(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserRatingTableDTO>> GetFriendsRatingTable(List<long> friendsIds)
        {
            throw new NotImplementedException();
        }

        public Task<List<ArtifactDTO>> GetUserArtifact(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<BackgroundDTO>> GetUserBackgrounds(long userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByVkId(string vk_user_id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserFoodDTO>> GetUserFood(long userId)
        {
            throw new NotImplementedException();
        }


        public Task<long> GetUserIdByVkId(string vk_user_id)
        {
            throw new NotImplementedException();
        }

    }
}
