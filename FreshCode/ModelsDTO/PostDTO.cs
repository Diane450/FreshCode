namespace FreshCode.ModelsDTO
{
    public class PostDTO
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string Title { get; set; } = null!;

        public DateTimeOffset CreatedAt { get; set; }

        public DateTimeOffset? UpdatedAt { get; set; }

        public DateTimeOffset? DeletedAt { get; set; }

        public long ViewsCount { get; set; }

        public List<PostBlockDTO> PostBlocks { get; set; } = null!;

        public int DislikesCount { get; set; }

        public int LikesCount { get; set; }

        public TagDTO Tag { get; set; } = null!;
    }
}
