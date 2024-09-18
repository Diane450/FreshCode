using FreshCode.DbModels;

namespace FreshCode.Interfaces
{
    public interface ICommentRepository
    {
        public IQueryable<PostComment> GetCommentsByPostId(long postId);

        public Task<PostComment> GetCommentById(long commentId);
    }
}
