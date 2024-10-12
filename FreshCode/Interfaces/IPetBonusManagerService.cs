using FreshCode.DbModels;
using FreshCode.Responses;

namespace FreshCode.Interfaces
{
    public interface IPetBonusManagerService
    {
        void SetBonuses(Pet pet, List<Bonu> bonuses);

        PetStatResponse GetBonuses(PetStatResponse petResponse, List<Bonu> bonuses);

        void RemoveBonuses(Pet pet, List<Bonu> bonuses);
    }
}
