using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FreshCode.Mappers
{
    public static class PostCommentMapper
    {
        public static CommentDTO ToDto(PostComment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                CreatedAt = comment.CreatedAt,
                Comment = comment.Comment,
                UserId = comment.UserId,
                UpdatedAt = comment.UpdatedAt
            };
        }
    }
}
