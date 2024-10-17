using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Interfaces
{
    public interface IBodyRepository
    {
        IQueryable<Body> GetBodiesAsync();
    }
}
