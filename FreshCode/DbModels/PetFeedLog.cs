using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class PetFeedLog
{
    public long Id { get; set; }

    public long PetId { get; set; }

    public DateTime CreatedAt { get; set; }

    public int FoodLevel { get; set; }

    public virtual Pet Pet { get; set; } = null!;
}
