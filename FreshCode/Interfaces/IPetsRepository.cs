using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.Requests;

namespace FreshCode.Interfaces
{
    public interface IPetsRepository
    {
        Task<PetDTO> GetPetInfoAsync(int userId);
        Task<PetDTO> LevelUpAsync(PetDTO pet);
        System.Threading.Tasks.Task ChangePetsArtifact(PetDTO pet);
        System.Threading.Tasks.Task FeedAsync(PetDTO pet);
    }
}
