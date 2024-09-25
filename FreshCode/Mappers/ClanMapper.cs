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

        public static ClanDTO ToDTO(Clan clan)
        {
            return new ClanDTO
            {
                Id = clan.Id,
                Name = clan.Name,
                WonBattlesCount = clan.WonBattlesCount,
                MemberCount = clan.UserClans.Count
            };
        }

        public static List<ClanDTO> ToDTO(List<Clan> clans)
        {
            List<ClanDTO> clanDTOs = new List<ClanDTO>();

            foreach (var clan in clans)
            {
                clanDTOs.Add(ToDTO(clan));
            }
            return clanDTOs;
        }
    }
}
