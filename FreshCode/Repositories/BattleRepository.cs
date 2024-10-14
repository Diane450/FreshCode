using FreshCode.DbModels;
using FreshCode.Interfaces;
using Microsoft.EntityFrameworkCore;
using Task = System.Threading.Tasks.Task;

namespace FreshCode.Repositories
{
    public class BattleRepository(FreshCodeContext dbContext) : IBattleRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public IQueryable<long> GetPetOpponents(Pet pet)
        {
            return _dbContext.BattleQueues
                .Where(bq => bq.PetLevel >= pet.Level.LevelValue - 1
                && bq.PetLevel <= pet.Level.LevelValue + 1)
                .Include(p => p.Pet)
                .Select(p => p.PetId);
        }
    }
}
