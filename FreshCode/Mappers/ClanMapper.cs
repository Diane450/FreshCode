using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Mappers
{
    public static class ClanMapper
    {
        public static ClanRatingTableDTO ToRatingTableDTO(Clan clan)
        {
            return new ClanRatingTableDTO
            {
                ClanName = clan.Name,
                WonBattlesCount = clan.WonBattlesCount
            };
        }
    }
}
