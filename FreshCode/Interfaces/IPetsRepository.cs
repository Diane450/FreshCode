using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.Requests;

namespace FreshCode.Interfaces
{
    public interface IPetsRepository
    {
        Task<PetDTO> GetPetByUserId(int userId);
        Task<PetDTO> LevelUpAsync(PetDTO pet);
        System.Threading.Tasks.Task ChangePetsArtifact(PetDTO pet);
        Task<Pet> GetPetById(long id);
        System.Threading.Tasks.Task SaveShangesAsync();
    }
}
