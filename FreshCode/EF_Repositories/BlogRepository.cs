using FreshCode.DbModels;
using FreshCode.EF_Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.EF_Repositories
{
    public class BlogRepository(FreshCodeContext dbContext) : IBlogRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;
        
        [HttpGet]
        public async Task<List<PostDTO>> GetAllPosts()
        {
            return await _dbContext.Posts
                .Select(p => PostMapper.ToDTO(p))
                .ToListAsync();
        }

        [HttpPost]
        public async System.Threading.Tasks.Task CreatePost(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
        }
    }
}
