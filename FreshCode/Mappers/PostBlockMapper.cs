using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FreshCode.Mappers
{
    public static class PostBlockMapper
    {
        public static PostBlockDTO ToDTO(PostBlock postBlock)
        {
            return new PostBlockDTO()
            {
                Id = postBlock.Id,
                Content = postBlock.Content,
                ContentTypeId = postBlock.ContentTypeId,
                Index = postBlock.Index,
            };
        }
    }
}
