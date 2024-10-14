using FreshCode.DbModels;

namespace FreshCode.Interfaces
{
    public interface IBattleRepository
    {
        public IQueryable<Pet> GetPetOpponents(long levelValue);
    }
}
