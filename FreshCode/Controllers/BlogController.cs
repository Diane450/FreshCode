using FreshCode.Models;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("blog")]
    public class BlogController(BlogUseCase blogUseCase) : BaseController
    {
        private readonly BlogUseCase _blogUseCase = blogUseCase;

        [HttpGet("posts")]
        public async Task<PagedList<PostDTO>> GetPosts([FromQuery] QueryParameters parameters)
        {
            return await _blogUseCase.GetPosts(parameters);
        }

        [HttpPost("posts/create")]
        public async Task CreatePost([FromBody] CreatePostRequest request)
        {
            long userId = GetUserId(HttpContext);
            await _blogUseCase.CreatePost(request, userId);
        }

        [HttpDelete("posts/{postId}")]
        public async Task DeletePost(int postId)
        {
            await _blogUseCase.DeletePost(postId);
        }

        [HttpGet("posts/{postId}/comments")]
        public async Task<PagedList<CommentDTO>> GetCommentsByPost([FromQuery] QueryParameters parameters, int postId)
        {
            return await _blogUseCase.GetCommentsByPostId(parameters, postId);
        }

        [HttpPost("posts/{postId}/comments")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request, int postId)
        {
            await _blogUseCase.CreateComment(request, postId);
            return Ok();
        }

        //[HttpPost]
        //public async Task GetPostStatistics([FromBody] long postId)
        //{
        //    await _blogUseCase.GetPostStatistics(postId);
        //}
    }
}
