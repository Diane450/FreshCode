using FreshCode.DbModels;
using FreshCode.Interfaces;

namespace FreshCode.Repositories
{
    public class ClanRepository(FreshCodeContext dbContext) : IClanRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public void DeleteClan(Clan clan)
        {

            _dbContext.Clans.Remove(clan);
        }
    }
}
