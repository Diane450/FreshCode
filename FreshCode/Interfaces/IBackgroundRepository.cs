using FreshCode.DbModels;

namespace FreshCode.Interfaces
{
    public interface IBackgroundRepository
    {
        public Task<Background> GetBackgroundById(long id);
    }
}
