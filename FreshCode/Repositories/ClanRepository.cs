using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class ClanRepository(FreshCodeContext dbContext) : IClanRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public async Task<Clan> GetClanById(long clanId)
        {
            var clan =  await _dbContext.Clans
                            .FindAsync(clanId);

            if (clan == null)
            {
                throw new Exception($"КЛан №{clanId} не найден");
            }
            return clan;
        }

        public async Task<List<ClanRatingTableDTO>> GetClanRatingTable()
        {
            return await _dbContext.Clans
                .Select(c => ClanMapper.ToRatingTableDTO(c))
                .ToListAsync();
        }
    }
}
