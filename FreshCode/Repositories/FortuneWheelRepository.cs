using FreshCode.DbModels;
using FreshCode.Interfaces;

namespace FreshCode.Repositories
{
    public class FortuneWheelRepository(FreshCodeContext dbContext) : IFortuneRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public DateTime? GetUserLastWheelRollTime(long userId)
        {
            IQueryable<UserFortuneWheelSpin> spins = _dbContext.UserFortuneWheelSpins
                .Where(f => f.UserId == userId);
            var spinsList = spins.OrderByDescending(f => f.Id).ToList();
            return spinsList.Count == 0 ? null : spinsList.ToList()[0].CreatedAt;
        }
    }
}