using FreshCode.ModelsDTO;
using FreshCode.Requests;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BlogController : Controller
    {
        private readonly BlogUseCase _blogUseCase;

        public BlogController(BlogUseCase blogUseCase)
        {
            _blogUseCase = blogUseCase;
        }

        [HttpGet]
        public async Task<List<PostDTO>> GetAllPosts()
        {
            return await _blogUseCase.GetAllPosts();
        }

        [HttpPost]
        public async Task CreatePost([FromBody] CreatePostRequest request)
        {
            var vk_user_id = await VkLaunchParamsService.GetParamValueAsync(Request.Headers, "vk_user_id");
            await _blogUseCase.CreatePost(request, vk_user_id);
        }

        [HttpPost]
        public async Task GetPostStatistics([FromBody] long postId)
        {
            await _blogUseCase.GetPostStatistics(postId);
        }
    }
}
