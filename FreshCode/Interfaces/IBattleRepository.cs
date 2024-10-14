using FreshCode.DbModels;

namespace FreshCode.Interfaces
{
    public interface IBattleRepository
    {
        public IQueryable<long> GetPetOpponents(Pet pet);
    }
}
