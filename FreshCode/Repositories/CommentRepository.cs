using FreshCode.DbModels;
using FreshCode.Interfaces;

namespace FreshCode.Repositories
{
    public class CommentRepository(FreshCodeContext dbContext) : ICommentRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public IQueryable<PostComment> GetCommentsByPostId(long postId)
        {
            return _dbContext.PostComments
                .Where(p=>p.PostId == postId);
        }
    }
}
