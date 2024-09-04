namespace FreshCode.ModelsDTO
{
    public class PostDTO
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string Title { get; set; } = null!;

        public DateOnly CreatedAt { get; set; }

        public DateOnly? UpdatedAt { get; set; }

        public DateOnly? DeletedAt { get; set; }

        public long TagId { get; set; }

        public long ViewsCount { get; set; }

        public List<CommentDTO>? Comments { get; set; }

        public List<PostBlockDTO> PostBlocks { get; set; } = null!;

        public List<RatingDTO>? Ratings { get; set; }

        public TagDTO? Tag { get; set; }
    }
}
