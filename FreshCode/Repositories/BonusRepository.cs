using FreshCode.DbModels;
using FreshCode.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class BonusRepository(FreshCodeContext dbContext) : IBonusRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public IQueryable<Bonu> GetAllBonusesAsync()
        {
            return _dbContext.Bonus
                 .Where(b => b.CharacteristicId != 6)
                .Include(b => b.Characteristic)
                .Include(b => b.Type);
        }
    }
}
