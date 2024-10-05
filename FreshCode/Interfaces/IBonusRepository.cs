using FreshCode.DbModels;

namespace FreshCode.Interfaces
{
    public interface IBonusRepository
    {
        IQueryable<Bonu> GetAllBonusesAsync();
    }
}
