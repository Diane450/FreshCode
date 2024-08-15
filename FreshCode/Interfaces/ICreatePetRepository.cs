using FreshCode.ModelsDTO;

namespace FreshCode.Interfaces
{
    public interface ICreatePetRepository
    {
        Task<List<BodyDTO>> GetBodiesAsync();
        Task<List<EyeDTO>> GetEyesAsync();
    }
}
