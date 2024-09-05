using FreshCode.DbModels;
using FreshCode.EF_Interfaces;
using FreshCode.ModelsDTO;
using FreshCode.Requests;

namespace FreshCode.UseCases
{
    public class BlogUseCase(IBlogRepository blogRepository, IUserRepository userRepository)
    {
        private readonly IBlogRepository _blogRepository = blogRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<List<PostDTO>> GetAllPosts()
        {
           return await _blogRepository.GetAllPosts();
        }

        public async System.Threading.Tasks.Task CreatePost(CreatePostRequest request, string? vk_user_id)
        {
            long userId = await _userRepository.GetUserIdByVkId(vk_user_id);
            Post post = new()
            {
                Title = request.Title,
                CreatedAt = request.CreatedAt,
                TagId = request.Tag.Id,
                UserId = userId,
            };
            await _blogRepository.CreatePost(post);
            await _userRepository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task GetPostStatistics(long postId)
        {
            
        }
    }
}
