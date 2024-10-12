using FreshCode.DbModels;
using FreshCode.Responses;

namespace FreshCode.Interfaces
{
    public interface IPetBonusManagerService
    {
        void SetBonuses(Pet pet, List<Bonu> bonuses);

        System.Threading.Tasks.Task SetBonus(Pet pet, Bonu bonus);

        PetStatResponse GetBonuses(PetStatResponse petResponse, List<Bonu> bonuses);

        void RemoveBonuses(Pet pet, List<Bonu> bonuses);
    }
}
