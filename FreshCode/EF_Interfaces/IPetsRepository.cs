using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.Requests;

namespace FreshCode.EF_Interfaces
{
    public interface IPetsRepository
    {
        Task<Pet> GetPetByUserId(long userId);
        System.Threading.Tasks.Task ChangePetsArtifact(PetDTO pet);
        Task<Pet> GetPetById(long id);
        System.Threading.Tasks.Task SaveShangesAsync();
        Task<Pet> CreatePetAsync(CreatePetRequest request, long userId);
        void UpdateAsync(Pet pet);
        Task<Level> GelLevelValues(long levelId);
    }
}
