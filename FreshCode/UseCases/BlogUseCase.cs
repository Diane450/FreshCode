
using FreshCode.DbModels;
using FreshCode.Extensions;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.Models;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;

namespace FreshCode.UseCases
{
    public class BlogUseCase(IBlogRepository blogRepository, ICommentRepository commentRepository, IBaseRepository baseRepository)
    {
        private readonly IBlogRepository _blogRepository = blogRepository;
        private readonly IBaseRepository _baseRepository = baseRepository;
        private readonly ICommentRepository _commentRepository = commentRepository;

        public async Task<PagedList<PostDTO>> GetPosts(QueryParameters parameters)
        {
            IQueryable<PostDTO> posts = _blogRepository.GetAllPosts()
                .Select(p=>new PostDTO()
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    Title = p.Title,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    DeletedAt = p.DeletedAt,
                    TagId = p.TagId,
                    ViewsCount = p.PostViews.Count
                });

            posts = posts.Sort(parameters.SortBy, parameters.SortDescending);

            var pagedListResult = await PagedList<PostDTO>.CreateAsync(posts, parameters.Page, parameters.PageSize);

            return pagedListResult;
        }

        public async System.Threading.Tasks.Task CreatePost(CreatePostRequest request, long userId)
        {
            Post post = new()
            {
                Title = request.Title,
                CreatedAt = request.CreatedAt,
                TagId = request.Tag.Id,
                UserId = userId,
            };
            await _baseRepository.AddAsync(post);
            await _baseRepository.SaveChangesAsync();
        }

        public async Task<PagedList<CommentDTO>> GetCommentsByPostId(QueryParameters parameters, long blogId)
        {
            IQueryable<CommentDTO> comments = _commentRepository.GetCommentsByPostId(blogId)
                .Select(c => new CommentDTO()
                {
                    Id = c.Id,
                    UserId= c.User.Id,
                    Comment = c.Comment,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                });
            comments = comments.Sort(parameters.SortBy, parameters.SortDescending);

            var pagedListResult = await PagedList<CommentDTO>.CreateAsync(comments, parameters.Page, parameters.PageSize);
            return pagedListResult;
        }

        public async System.Threading.Tasks.Task DeletePost(long postId)
        {
            var post = await _blogRepository.GetPostById(postId);

            post.DeletedAt = DateTime.UtcNow;

            await _baseRepository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task CreateComment(CreateCommentRequest request, long postId)
        {
            PostComment postComment = new PostComment()
            {
                Comment = request.Comment,
                UserId = request.UserId,
                CreatedAt = DateTime.UtcNow,
                PostId = postId
            };

            await _baseRepository.AddAsync(postComment);
            await _baseRepository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task EditComment(string newText, long commentId)
        {
            PostComment comment = await _commentRepository.GetCommentById(commentId);
            comment.Comment = newText;
            await _baseRepository.SaveChangesAsync();
        }
    }
}
