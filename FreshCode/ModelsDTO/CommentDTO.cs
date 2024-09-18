namespace FreshCode.ModelsDTO
{
    public class CommentDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Comment { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
