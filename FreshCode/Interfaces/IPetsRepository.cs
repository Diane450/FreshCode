using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.Requests;

namespace FreshCode.Interfaces
{
    public interface IPetsRepository
    {
        Task <Pet> CreatePetAsync(CreatePetRequest request, string? vk_user_id);
        Task<PetDTO> GetPetInfoAsync(int userId);
        Task<PetDTO> LevelUpAsync(PetDTO pet);
    }
}
