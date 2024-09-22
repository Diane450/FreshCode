using FreshCode.ModelsDTO;

namespace FreshCode.Requests
{
    public class CreatePostRequest
    {
        public string Title { get; set; } = null!;
        
        public DateTime CreatedAt { get; set; }

        public List<PostBlockDTO> PostBlock { get; set; } = null!;

        public long TagId { get; set; }
    }
}
