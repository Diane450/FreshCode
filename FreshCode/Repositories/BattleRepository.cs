using FreshCode.DbModels;
using FreshCode.Interfaces;

namespace FreshCode.Repositories
{
    public class BattleRepository(FreshCodeContext dbContext) : IBattleRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public IQueryable<long> GetPetOpponents(Pet pet)
        {
            return _dbContext.Pets
                .Where(p => p.LevelId <= pet.Level.LevelValue + 1
                && p.LevelId >= pet.Level.LevelValue - 1
                && p.Id != pet.Id
                && !p.IsSleeping)
                .Select(p=>p.Id);
        }
    }
}
