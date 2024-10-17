using FreshCode.DbModels;
using FreshCode.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace FreshCode.Repositories
{
    public class BattleRepository(FreshCodeContext dbContext) : IBattleRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public async Task<UserBattle> GetBattleById(long battleId)
        {
            return await _dbContext.UserBattles
                .FindAsync(battleId);
        }
    }
}
