using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class PetSleepLog
{
    public long Id { get; set; }

    public long PetId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime WokeUpAt { get; set; }

    public int InitialSleepNeed { get; set; }

    public virtual Pet Pet { get; set; } = null!;
}
