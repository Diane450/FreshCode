namespace FreshCode.ModelsDTO
{
    public class PostBlockDTO
    {
        public long Id { get; set; }

        public long ContentTypeId { get; set; }

        public string Content { get; set; } = null!;

        public int Index { get; set; }
    }
}
