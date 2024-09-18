using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Interfaces
{
    public interface IBlogRepository
    {
        IQueryable<Post> GetAllPosts();

        Task<Post> GetPostById(long id);
    }
}
