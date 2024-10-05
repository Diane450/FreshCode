using FreshCode.DbModels;

namespace FreshCode.Interfaces
{
    public interface IPetBonusManagerService
    {
        void SetBonuses(Pet pet, List<Bonu> bonuses);

        void RemoveBonuses(Pet pet, List<Bonu> bonuses);
    }
}
