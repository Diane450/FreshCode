using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FreshCode.Mappers
{
    public static class PostMapper
    {
        public static PostDTO ToDTO(Post post)
        {
            return null;
            //return new PostDTO
            //{
            //    Id = post.Id,
            //    Title = post.Title,
            //    CreatedAt = post.CreatedAt,
            //    UpdatedAt = post.UpdatedAt,
            //    DeletedAt = post.DeletedAt,
            //    TagId = post.TagId,
            //    ViewsCount = post.PostViews.Count,
            //    Ratings = post.PostRatings,
            //    Tag = post.Tag,
            //    PostBlocks = post.PostBlocks
            //};
        }
    }
}
