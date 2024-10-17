using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.Requests;

namespace FreshCode.Interfaces
{
    public interface IEyesRepository
    {
        IQueryable<Eye> GetEyesAsync();
    }
}
