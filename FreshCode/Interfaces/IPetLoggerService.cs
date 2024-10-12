using FreshCode.DbModels;

namespace FreshCode.Interfaces
{
    public interface IPetLoggerService
    {
        public System.Threading.Tasks.Task CreateSleepLog(Pet pet);

        public System.Threading.Tasks.Task CreateFeedLog(Pet pet);

    }
}
