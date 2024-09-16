
using FreshCode.DbModels;
using FreshCode.Extensions;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.Models;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.UseCases
{
    public class BlogUseCase(IBlogRepository blogRepository, IUserRepository userRepository, IBaseRepository baseRepository)
    {
        private readonly IBlogRepository _blogRepository = blogRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IBaseRepository _baseRepository = baseRepository;

        public async Task<List<PostDTO>> GetPosts(QueryParameters parameters)
        {
            IQueryable<Post>posts = await _blogRepository.GetAllPosts();

            posts = posts.Sort(parameters.SortBy, parameters.SortDescending);

            var pagedOrders = await posts
                .Paginate(parameters.Page, parameters.PageSize)
                .ToListAsync();

            var pagedPosts = new PagedResult<Post>(pagedOrders, parameters.Page, parameters.PageSize);
            var result = PostMapper.ToDTO(pagedPosts.Items);
            return result;
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
            await _blogRepository.CreatePost(post);
            await _baseRepository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task GetPostStatistics(long postId)
        {

        }
    }
}
