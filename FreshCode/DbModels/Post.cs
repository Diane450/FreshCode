using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Post
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Title { get; set; } = null!;

    public long TagId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<PostBlock> PostBlocks { get; set; } = new List<PostBlock>();

    public virtual ICollection<PostComment> PostComments { get; set; } = new List<PostComment>();

    public virtual ICollection<PostRating> PostRatings { get; set; } = new List<PostRating>();

    public virtual ICollection<PostView> PostViews { get; set; } = new List<PostView>();

    public virtual Tag Tag { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
