using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.Interfaces
{
    public interface IBlogRepository
    {
        System.Threading.Tasks.Task CreatePost(Post post);
        Task<List<PostDTO>> GetAllPosts();
    }
}
