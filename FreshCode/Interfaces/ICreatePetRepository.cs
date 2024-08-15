using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.Requests;

namespace FreshCode.Interfaces
{
    public interface ICreatePetRepository
    {
        Task<Pet> CreatePetAsync(CreatePetRequest request, string? vk_user_id);
        Task<List<BodyDTO>> GetBodiesAsync();
        Task<List<EyeDTO>> GetEyesAsync();
    }
}
