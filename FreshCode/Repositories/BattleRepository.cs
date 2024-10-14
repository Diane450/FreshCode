using FreshCode.DbModels;
using FreshCode.Interfaces;

namespace FreshCode.Repositories
{
    public class BattleRepository(FreshCodeContext dbContext) : IBattleRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public IQueryable<Pet> GetPetOpponents(long levelValue)
        {
            return _dbContext.Pets
                .Where(p => p.LevelId <= levelValue + 1 && p.LevelId >= levelValue - 1);
        }
    }
}
