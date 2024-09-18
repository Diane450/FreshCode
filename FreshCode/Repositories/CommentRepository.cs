using FreshCode.DbModels;
using FreshCode.Interfaces;

namespace FreshCode.Repositories
{
    public class CommentRepository(FreshCodeContext dbContext) : ICommentRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public async Task<PostComment> GetCommentById(long commentId)
        {
            return await _dbContext.PostComments
                .FindAsync(commentId);
        }

        public IQueryable<PostComment> GetCommentsByPostId(long postId)
        {
            return _dbContext.PostComments
                .Where(p => p.PostId == postId);
        }


    }
}
