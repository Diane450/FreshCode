namespace FreshCode.Interfaces
{
    public interface IBaseRepository
    {
        public void Remove<T>(T entity) where T : class;
        
        public void RemoveRange<T>(List<T> entities) where T : class;

        public System.Threading.Tasks.Task AddAsync<T>(T entity) where T : class;

        public void Update<T>(T entity) where T : class;

        public System.Threading.Tasks.Task SaveChangesAsync();
    }
}
