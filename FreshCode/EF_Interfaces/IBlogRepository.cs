using FreshCode.DbModels;
using FreshCode.ModelsDTO;

namespace FreshCode.EF_Interfaces
{
    public interface IBlogRepository
    {
        System.Threading.Tasks.Task CreatePost(Post post);
        Task<List<PostDTO>> GetAllPosts();
    }
}
