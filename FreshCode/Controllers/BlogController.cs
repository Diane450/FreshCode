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
        public async Task<ActionResult<PagedList<PostDTO>>> GetPosts([FromQuery] QueryParameters parameters)
        {
            return await _blogUseCase.GetPosts(parameters);
        }

        [HttpPost("posts/create")]
        public async Task<ActionResult<PostDTO>> CreatePost([FromBody] CreatePostRequest request)
        {
            long userId = GetUserId(HttpContext);
            return await _blogUseCase.CreatePost(request, userId);
        }

        [HttpDelete("posts/{postId}")]
        public async Task<ActionResult> DeletePost(long postId)
        {
            await _blogUseCase.DeletePost(postId);
            return Ok();
        }

        [HttpGet("posts/{postId}/comments")]
        public async Task<PagedList<CommentDTO>> GetCommentsByPost([FromQuery] QueryParameters parameters, long postId)
        {
            return await _blogUseCase.GetCommentsByPostId(parameters, postId);
        }

        [HttpPost("posts/{postId}/comments")]
        public async Task<CommentDTO> CreateComment([FromBody] CreateCommentRequest request, long postId)
        {
            return await _blogUseCase.CreateComment(request, postId);
        }

        [HttpPut("comments/{commentId}")]
        public async Task<CommentDTO> EditComment([FromBody] string newText, long commentId)
        {
            return await _blogUseCase.EditComment(newText, commentId);
        }

        [HttpPost("post/{postId}/reaction")]
        public async Task<int> AddReactionToPost([FromBody] bool reactionValue, long postId)
        {
            var userId = GetUserId(HttpContext);
            return await _blogUseCase.AddReactionToPost(userId, postId, reactionValue);
        }

        [HttpPut("posts/{postId}")]
        public async Task<PostDTO> EditPost([FromBody] List<PostBlockDTO> blocks, long postId)
        {
            return await _blogUseCase.EditPost(blocks, postId);
        }
    }
}
