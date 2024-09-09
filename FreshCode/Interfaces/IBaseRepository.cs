namespace FreshCode.Interfaces
{
    public interface IBaseRepository
    {
        public void DeleteAsync<T>(T entity) where T : class;

        public System.Threading.Tasks.Task AddAsync<T>(T entity) where T : class;

        public System.Threading.Tasks.Task SaveChangesAsync();
    }
}
