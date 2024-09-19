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
        public async Task DeletePost(long postId)
        {
            await _blogUseCase.DeletePost(postId);
        }

        [HttpGet("posts/{postId}/comments")]
        public async Task<PagedList<CommentDTO>> GetCommentsByPost([FromQuery] QueryParameters parameters, long postId)
        {
            return await _blogUseCase.GetCommentsByPostId(parameters, postId);
        }

        [HttpPost("posts/{postId}/comments")]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request, long postId)
        {
            await _blogUseCase.CreateComment(request, postId);
            return Ok();
        }

        [HttpPut("comments/{commentId}")]
        public async Task<IActionResult> EditComment([FromBody] string newText, long commentId)
        {
            await _blogUseCase.EditComment(newText, commentId);
            return Ok();
        }

        [HttpPost("post/{postId}/reaction/{reactionValue}")]
        public async Task<IActionResult> AddReactionToPost(long postId, bool reactionValue)
        {
            var userId = GetUserId(HttpContext);
            await _blogUseCase.AddReactionToPost(userId, postId, reactionValue);
            return Ok();
        }



        //[HttpPut("posts/{postId}")]
        //public async Task<IActionResult> EditPost([FromBody] List<PostBlockDTO> blocks, int postId)
        //{
        //    await _blogUseCase.EditPost(blocks, postId);
        //    return Ok();
        //}
    }
}
