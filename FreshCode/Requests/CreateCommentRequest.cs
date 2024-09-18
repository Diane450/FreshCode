namespace FreshCode.Requests
{
    public class CreateCommentRequest
    {
        public long UserId { get; set; }
        public string Comment { get; set; } = null!;
    }
}
