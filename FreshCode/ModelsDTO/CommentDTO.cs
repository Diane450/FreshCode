namespace FreshCode.ModelsDTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Comment { get; set; } = null!;
        public int CreatedAt { get; set; }
        public int UpdatedAt { get; set; }
    }
}
