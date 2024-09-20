using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Interfaces
{
    public interface IBlogRepository
    {
        IQueryable<Post> GetAllPosts();
        Task<List<PostBlock>> GetPostBlocks(long postId);
        Task<Post> GetPostById(long id);
    }
}
