using FreshCode.ModelsDTO;

namespace FreshCode.Interfaces
{
    public interface ICreatePetRepository
    {
        Task<List<EyeDTO>> GetEyesAsync();
    }
}
