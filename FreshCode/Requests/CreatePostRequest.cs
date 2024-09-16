using FreshCode.ModelsDTO;

namespace FreshCode.Requests
{
    public class CreatePostRequest
    {
        public string Title { get; set; } = null!;
        
        public DateTimeOffset CreatedAt { get; set; }

        public TagDTO Tag { get; set; } = null!;
    }
}
