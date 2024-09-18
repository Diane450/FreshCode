using FreshCode.DbModels;

namespace FreshCode.Interfaces
{
    public interface ICommentRepository
    {
        public IQueryable<PostComment> GetCommentsByPostId(long postId);
    }
}
