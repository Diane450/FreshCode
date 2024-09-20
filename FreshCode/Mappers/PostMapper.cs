using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FreshCode.Mappers
{
    public static class PostMapper
    {
        public static PostDTO ToDTO(Post post)
        {
            return new PostDTO
            {
                Id = post.Id,
                UserId = post.UserId,
                Title = post.Title,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt,
                DeletedAt = post.DeletedAt,
                TagId = post.TagId,
                ViewsCount = post.PostViews.Count,
                DislikesCount = post.PostRatings.Where(post => post.Rating == false).Count(),
                LikesCount = post.PostRatings.Where(post => post.Rating == true).Count(),
                PostBlocks = post.PostBlocks.Select(pb=>PostBlockMapper.ToDTO(pb)).ToList()
            };
        }
        public static List<PostDTO> ToDTO(List<Post> posts)
        {
            List<PostDTO> postsDTO = new List<PostDTO>();
            foreach (var post in posts)
            {
                postsDTO.Add(ToDTO(post));
            }
            return postsDTO;
        }
    }
}
