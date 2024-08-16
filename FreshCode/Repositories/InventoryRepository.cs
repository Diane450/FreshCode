using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.ModelsDTO;

namespace FreshCode.Repositories
{
    public class InventoryRepository(FreshCodeContext dbContext) : IInventoryRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public async System.Threading.Tasks.Task SetBackground(User user)
        {
        }
    }
}
