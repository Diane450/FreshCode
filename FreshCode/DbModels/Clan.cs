using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class Clan
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public int WonBattlesCount { get; set; }

    public decimal AverageClanPower { get; set; }

    public long CreatorId { get; set; }

    public virtual ICollection<ClanBattle> ClanBattleFirstClans { get; set; } = new List<ClanBattle>();

    public virtual ICollection<ClanBattle> ClanBattleSecondClans { get; set; } = new List<ClanBattle>();

    public virtual ICollection<ClanBattle> ClanBattleWinners { get; set; } = new List<ClanBattle>();

    public virtual User Creator { get; set; } = null!;

    public virtual ICollection<UserClan> UserClans { get; set; } = new List<UserClan>();
}
