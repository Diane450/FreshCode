using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Hosting;

namespace FreshCode.Mappers
{
    public class CommentMapper
    {
        public static CommentDTO ToDTO(PostComment c)
        {
            return new CommentDTO()
            {
                Id = c.Id,
                UserId = c.UserId,
                Comment = c.Comment,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
            };
        }

        public static List<CommentDTO> ToDTO(List<PostComment> comments)
        {
            List<CommentDTO> commentsDTO = new List<CommentDTO>();
            foreach (var post in comments)
            {
                commentsDTO.Add(ToDTO(post));
            }
            return commentsDTO;
        }
    }
}
