using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Interfaces
{
    public interface IClanRepository
    {
        Task<List<ClanRatingTableDTO>> GetClanRatingTable();
    }
}
