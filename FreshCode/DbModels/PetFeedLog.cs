using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class PetFeedLog
{
    public long Id { get; set; }

    public long PetId { get; set; }

    public long FoodId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Food Food { get; set; } = null!;
}
