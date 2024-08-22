using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Interfaces
{
    public interface IClanRepository
    {
        void DeleteClan(Clan clan);
        Task<List<ClanRatingTableDTO>> GetClanRatingTable();
    }
}
