using System;
using System.Collections.Generic;

namespace FreshCode.DbModels;

public partial class User
{
    public long Id { get; set; }

    public int Money { get; set; }

    public int StatPoints { get; set; }

    public long BackgroundId { get; set; }

    public int WonBattlesCount { get; set; }

    public int PrimogemsCount { get; set; }

    public int FatesCount { get; set; }

    public int VkId { get; set; }

    public virtual ICollection<ArtifactHistory> ArtifactHistories { get; set; } = new List<ArtifactHistory>();

    public virtual Background Background { get; set; } = null!;

    public virtual ICollection<Clan> Clans { get; set; } = new List<Clan>();

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();

    public virtual ICollection<PostComment> PostComments { get; set; } = new List<PostComment>();

    public virtual ICollection<PostRating> PostRatings { get; set; } = new List<PostRating>();

    public virtual ICollection<PostView> PostViews { get; set; } = new List<PostView>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<UserArtifact> UserArtifacts { get; set; } = new List<UserArtifact>();

    public virtual ICollection<UserBackground> UserBackgrounds { get; set; } = new List<UserBackground>();

    public virtual ICollection<UserBattle> UserBattleFirstPlayers { get; set; } = new List<UserBattle>();

    public virtual ICollection<UserBattle> UserBattleSecondPlayers { get; set; } = new List<UserBattle>();

    public virtual ICollection<UserBattle> UserBattleWinners { get; set; } = new List<UserBattle>();

    public virtual ICollection<UserClan> UserClans { get; set; } = new List<UserClan>();

    public virtual ICollection<UserFood> UserFoods { get; set; } = new List<UserFood>();

    public virtual ICollection<UserFortuneWheelSpin> UserFortuneWheelSpins { get; set; } = new List<UserFortuneWheelSpin>();

    public virtual ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();
}
