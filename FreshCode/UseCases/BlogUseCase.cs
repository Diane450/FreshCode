
using FreshCode.DbModels;
using FreshCode.Extensions;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.Models;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace FreshCode.UseCases
{
    public class BlogUseCase(IBlogRepository blogRepository, IUserRepository userRepository, IBaseRepository baseRepository)
    {
        private readonly IBlogRepository _blogRepository = blogRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IBaseRepository _baseRepository = baseRepository;

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
                })
                ;

            posts = posts.Sort(parameters.SortBy, parameters.SortDescending);

            var pagedListResult = await PagedList<PostDTO>.CreateAsync(posts, parameters.Page, parameters.PageSize);

            return pagedListResult;
        }

        public async System.Threading.Tasks.Task CreatePost(CreatePostRequest request, long userId)
        {
            Post post = new()
            {
                Title = request.Title,
                CreatedAt = DateTime.UtcNow,
                TagId = request.Tag.Id,
                UserId = userId,
            };
            await _blogRepository.CreatePost(post);
            await _baseRepository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task GetPostStatistics(long postId)
        {

        }
    }
}
