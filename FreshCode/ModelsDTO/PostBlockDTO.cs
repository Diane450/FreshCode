namespace FreshCode.ModelsDTO
{
    public class PostBlockDTO
    {
        public int Id { get; set; }

        public string Content_Type { get; set; } = null!;

        public string Content { get; set; } = null!;

        public int Index { get; set; }
    }
}
