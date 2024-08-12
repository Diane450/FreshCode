using FreshCode.DbModels;
using FreshCode.Requests;

namespace FreshCode.Interfaces
{
    public interface IPetsRepository
    {
        Task<Pet> CreatePetAsync(CreatePetRequest request, string? vk_user_id);
        Task<Pet> GetPetInfoAsync(int userId);
        Task<Pet> LevelUpAsync(Pet pet);
    }
}
