using FreshCode.DbModels;

namespace FreshCode.Interfaces
{
    public interface IBattleRepository
    {
        Task<UserBattle> GetBattleById(long battleId);
    }
}
