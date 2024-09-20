using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.Repositories
{
    public class BlogRepository(FreshCodeContext dbContext) : IBlogRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public IQueryable<Post> GetAllPosts()
        {
            return _dbContext.Posts
                .Include(p => p.Tag)
                .Include(p => p.PostViews)
                .Include(p => p.PostRatings)
                .Include(p => p.PostBlocks)
                .ThenInclude(pb => pb.ContentType);
        }

        public async Task<Post> GetPostById(long id)
        {
            return await _dbContext.Posts
                .FindAsync(id);
        }

        public Task<List<PostBlock>> GetPostBlocks(long postId)
        {
            return _dbContext.PostBlocks
                .Where(pb=>pb.PostId == postId).ToListAsync();
        }
    }
}
