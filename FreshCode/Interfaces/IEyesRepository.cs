using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.Requests;

namespace FreshCode.Interfaces
{
    public interface IEyesRepository
    {
        Task<List<EyeDTO>> GetEyesAsync();
        Task<Eye> GetEyesById(long id);
    }
}
