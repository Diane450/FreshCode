using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.Requests;

namespace FreshCode.Interfaces
{
    public interface IPetsRepository
    {
        Task<PetDTO> GetPetByUserId(long userId);
        Task<PetDTO> LevelUpAsync(Pet pet);
        System.Threading.Tasks.Task ChangePetsArtifact(PetDTO pet);
        Task<Pet> GetPetById(long id);
        System.Threading.Tasks.Task SaveShangesAsync();
        Task<Pet> CreatePetAsync(CreatePetRequest request, long userId);
        void UpdateAsync(Pet pet);
        Task<Level> GelLevelValues(long levelId);
    }
}
