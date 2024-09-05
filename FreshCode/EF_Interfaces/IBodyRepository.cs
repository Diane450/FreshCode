using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.EF_Interfaces
{
    public interface IBodyRepository
    {
        Task<List<BodyDTO>> GetBodiesAsync();
        Task<Body> GetBodyById(long id);
    }
}
