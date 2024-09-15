using FreshCode.ModelsDTO;
using FreshCode.Requests;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BlogController : BaseController
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
            long userId = GetUserId();
            await _blogUseCase.CreatePost(request, userId);
        }

        [HttpPost]
        public async Task GetPostStatistics([FromBody] long postId)
        {
            await _blogUseCase.GetPostStatistics(postId);
        }
    }
}
