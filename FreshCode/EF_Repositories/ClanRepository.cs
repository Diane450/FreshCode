using FreshCode.DbModels;
using FreshCode.EF_Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.EF_Repositories
{
    public class ClanRepository(FreshCodeContext dbContext) : IClanRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public void DeleteClan(Clan clan)
        {
            _dbContext.Clans.Remove(clan);
        }

        public async Task<List<ClanRatingTableDTO>> GetClanRatingTable()
        {
            return await _dbContext.Clans
                .Select(c=>ClanMapper.ToRatingTableDTO(c))
                .ToListAsync();
        }
    }
}
