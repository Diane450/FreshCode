using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class PetFeedLog
{
    public long Id { get; set; }

    public long PetId { get; set; }

    public DateTime CreatedAt { get; set; }
}
