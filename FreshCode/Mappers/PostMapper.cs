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
                Title = post.Title,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt,
                DeletedAt = post.DeletedAt,
                Tag = new TagDTO
                {
                    Id = post.TagId,
                    Tag = post.Tag.Tag1
                },
                ViewsCount = post.PostViews.Count
                //Ratings = post.PostRatings,
                //Tag = post.Tag,
                //PostBlocks = post.PostBlocks
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
