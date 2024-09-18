﻿
using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.ModelsDTO;
using FreshCode.Requests;

namespace FreshCode.UseCases
{
    public class BlogUseCase(IBlogRepository blogRepository, IUserRepository userRepository, IBaseRepository baseRepository)
    {
        private readonly IBlogRepository _blogRepository = blogRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IBaseRepository _baseRepository = baseRepository;

        public async Task<List<PostDTO>> GetAllPosts()
        {
           return await _blogRepository.GetAllPosts();
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
