using FreshCode.Models;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class BlogController(BlogUseCase blogUseCase) : BaseController
    {
        private readonly BlogUseCase _blogUseCase = blogUseCase;

        [HttpGet]
        public async Task<List<PostDTO>> GetPosts([FromQuery] QueryParameters parameters)
        {
            return await _blogUseCase.GetPosts(parameters);
        }

        [HttpPost]
        public async Task CreatePost([FromBody] CreatePostRequest request)
        {
            long userId = GetUserId(HttpContext);
            await _blogUseCase.CreatePost(request, userId);
        }

        [HttpPost]
        public async Task GetPostStatistics([FromBody] long postId)
        {
            await _blogUseCase.GetPostStatistics(postId);
        }
    }
}
