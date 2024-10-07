using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.DbModels;

public partial class FreshCodeContext : DbContext
{
    public FreshCodeContext()
    {
    }

    public FreshCodeContext(DbContextOptions<FreshCodeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artifact> Artifacts { get; set; }

    public virtual DbSet<ArtifactBonuse> ArtifactBonuses { get; set; }

    public virtual DbSet<ArtifactHistory> ArtifactHistories { get; set; }

    public virtual DbSet<ArtifactType> ArtifactTypes { get; set; }

    public virtual DbSet<Background> Backgrounds { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<BannerItem> BannerItems { get; set; }

    public virtual DbSet<BannerType> BannerTypes { get; set; }

    public virtual DbSet<Body> Bodies { get; set; }

    public virtual DbSet<Bonu> Bonus { get; set; }

    public virtual DbSet<BonusType> BonusTypes { get; set; }

    public virtual DbSet<Characteristic> Characteristics { get; set; }

    public virtual DbSet<Clan> Clans { get; set; }

    public virtual DbSet<ClanBattle> ClanBattles { get; set; }

    public virtual DbSet<ContentType> ContentTypes { get; set; }

    public virtual DbSet<Eye> Eyes { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<FoodBonuse> FoodBonuses { get; set; }

    public virtual DbSet<FortuneWheelResult> FortuneWheelResults { get; set; }

    public virtual DbSet<Level> Levels { get; set; }

    public virtual DbSet<Pet> Pets { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostBlock> PostBlocks { get; set; }

    public virtual DbSet<PostComment> PostComments { get; set; }

    public virtual DbSet<PostRating> PostRatings { get; set; }

    public virtual DbSet<PostView> PostViews { get; set; }

    public virtual DbSet<Rarity> Rarities { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserArtifact> UserArtifacts { get; set; }

    public virtual DbSet<UserBackground> UserBackgrounds { get; set; }

    public virtual DbSet<UserBattle> UserBattles { get; set; }

    public virtual DbSet<UserClan> UserClans { get; set; }

    public virtual DbSet<UserFood> UserFoods { get; set; }

    public virtual DbSet<UserTask> UserTasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=FreshCode;Username=postgres;Password=hepl16178");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artifact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Artifact _pkey");

            entity.ToTable("Artifact");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Artifact _Id_seq\"'::regclass)");
            entity.Property(e => e.ArtifactTypeId).HasColumnName("ArtifactType_Id");
            entity.Property(e => e.RarityId).HasColumnName("Rarity_Id");

            entity.HasOne(d => d.ArtifactType).WithMany(p => p.Artifacts)
                .HasForeignKey(d => d.ArtifactTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Artifact_ArtifactType");

            entity.HasOne(d => d.Rarity).WithMany(p => p.Artifacts)
                .HasForeignKey(d => d.RarityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Artifact_Rarity");
        });

        modelBuilder.Entity<ArtifactBonuse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Artifact_Bonuses_pkey");

            entity.ToTable("Artifact_Bonuses");

            entity.Property(e => e.ArtifactId).HasColumnName("Artifact_Id");
            entity.Property(e => e.BonusId).HasColumnName("Bonus_Id");

            entity.HasOne(d => d.Artifact).WithMany(p => p.ArtifactBonuses)
                .HasForeignKey(d => d.ArtifactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Artifact_Bonuses_Artifact");

            entity.HasOne(d => d.Bonus).WithMany(p => p.ArtifactBonuses)
                .HasForeignKey(d => d.BonusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Artifact_Bonuses_Bonus");
        });

        modelBuilder.Entity<ArtifactHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ArtifactHistory_pkey");

            entity.ToTable("ArtifactHistory");

            entity.Property(e => e.ArtifactId).HasColumnName("Artifact_Id");
            entity.Property(e => e.GotAt).HasColumnName("Got_at");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Artifact).WithMany(p => p.ArtifactHistories)
                .HasForeignKey(d => d.ArtifactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ArtifactHistory_Artifact");

            entity.HasOne(d => d.Banner).WithMany(p => p.ArtifactHistories)
                .HasForeignKey(d => d.BannerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ArtifactHistory_Banner");

            entity.HasOne(d => d.User).WithMany(p => p.ArtifactHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ArtifactHistory_User");
        });

        modelBuilder.Entity<ArtifactType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("artifacttype_pkey");

            entity.ToTable("ArtifactType");

            entity.Property(e => e.Id).HasDefaultValueSql("nextval('artifacttype_id_seq'::regclass)");
            entity.Property(e => e.Type).HasMaxLength(45);
        });

        modelBuilder.Entity<Background>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Background_pkey");

            entity.ToTable("Background");
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Banner_pkey");

            entity.ToTable("Banner");

            entity.HasOne(d => d.BannerType).WithMany(p => p.Banners)
                .HasForeignKey(d => d.BannerTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Banner_BannerType");
        });

        modelBuilder.Entity<BannerItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Banner_Item_pkey");

            entity.ToTable("Banner_Item");

            entity.Property(e => e.ArtifactId).HasColumnName("Artifact_Id");
            entity.Property(e => e.BannerId).HasColumnName("Banner_Id");

            entity.HasOne(d => d.Artifact).WithMany(p => p.BannerItems)
                .HasForeignKey(d => d.ArtifactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Banner_Item_Artifact");

            entity.HasOne(d => d.Banner).WithMany(p => p.BannerItems)
                .HasForeignKey(d => d.BannerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Banner_Item_Banner");
        });

        modelBuilder.Entity<BannerType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BannerType_pkey");

            entity.ToTable("BannerType");

            entity.Property(e => e.Type).HasColumnType("character varying");
        });

        modelBuilder.Entity<Body>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Body_pkey");

            entity.ToTable("Body");
        });

        modelBuilder.Entity<Bonu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Bonus_pkey");

            entity.Property(e => e.CharacteristicId).HasColumnName("Characteristic_Id");

            entity.HasOne(d => d.Characteristic).WithMany(p => p.Bonus)
                .HasForeignKey(d => d.CharacteristicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Bonus_Characteristic");

            entity.HasOne(d => d.Type).WithMany(p => p.Bonus)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Bonus_BonusType");
        });

        modelBuilder.Entity<BonusType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BonusType_pkey");

            entity.ToTable("BonusType");

            entity.Property(e => e.Type).HasColumnType("character varying");
        });

        modelBuilder.Entity<Characteristic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Characteristics_pkey");

            entity.Property(e => e.Characteristic1)
                .HasMaxLength(100)
                .HasColumnName("Characteristic");
        });

        modelBuilder.Entity<Clan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Clan_pkey");

            entity.ToTable("Clan");

            entity.Property(e => e.AverageClanPower).HasPrecision(10, 2);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Creator).WithMany(p => p.Clans)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clan_User");
        });

        modelBuilder.Entity<ClanBattle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ClanBattles_pkey");

            entity.Property(e => e.FirstClanId).HasColumnName("FirstClan_Id");
            entity.Property(e => e.SecondClanId).HasColumnName("SecondClan_Id");
            entity.Property(e => e.WinnerId).HasColumnName("Winner_Id");

            entity.HasOne(d => d.FirstClan).WithMany(p => p.ClanBattleFirstClans)
                .HasForeignKey(d => d.FirstClanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ClanBattles_FirstClan");

            entity.HasOne(d => d.SecondClan).WithMany(p => p.ClanBattleSecondClans)
                .HasForeignKey(d => d.SecondClanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ClanBattles_SecondClan");

            entity.HasOne(d => d.Winner).WithMany(p => p.ClanBattleWinners)
                .HasForeignKey(d => d.WinnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ClanBattles_Winner");
        });

        modelBuilder.Entity<ContentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Content_type_pkey");

            entity.ToTable("Content_type");

            entity.Property(e => e.Type).HasColumnType("character varying");
        });

        modelBuilder.Entity<Eye>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("eyes_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('eyes_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.X).HasColumnName("x");
            entity.Property(e => e.Y).HasColumnName("y");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Food_pkey");

            entity.ToTable("Food");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<FoodBonuse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Food_Bonuses_pkey");

            entity.ToTable("Food_Bonuses");

            entity.Property(e => e.BonusId).HasColumnName("Bonus_Id");
            entity.Property(e => e.FoodId).HasColumnName("Food_Id");

            entity.HasOne(d => d.Bonus).WithMany(p => p.FoodBonuses)
                .HasForeignKey(d => d.BonusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Food_Bonuses_Bonus");

            entity.HasOne(d => d.Food).WithMany(p => p.FoodBonuses)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Food_Bonuses_Food");
        });

        modelBuilder.Entity<FortuneWheelResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("FortuneWheelResults_pkey");

            entity.Property(e => e.BonusId).HasColumnName("Bonus_Id");
            entity.Property(e => e.CreatedAt).HasColumnName("Created_at");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Bonus).WithMany(p => p.FortuneWheelResults)
                .HasForeignKey(d => d.BonusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FortuneWheelResults_Bonus");

            entity.HasOne(d => d.User).WithMany(p => p.FortuneWheelResults)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FortuneWheelResults_User");
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Level_pkey");

            entity.ToTable("Level");

            entity.Property(e => e.EnhancementCoefficient).HasPrecision(10, 2);
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Pets_pkey");

            entity.Property(e => e.AccessoryId).HasColumnName("Accessory_Id");
            entity.Property(e => e.AveragePower).HasPrecision(1000, 2);
            entity.Property(e => e.BodyId).HasColumnName("Body_Id");
            entity.Property(e => e.CurrentCriticalChance).HasPrecision(10, 2);
            entity.Property(e => e.CurrentCriticalDamage).HasPrecision(10, 2);
            entity.Property(e => e.EyesId).HasColumnName("Eyes_Id");
            entity.Property(e => e.GeneralHappiness).HasPrecision(10, 2);
            entity.Property(e => e.HatId).HasColumnName("Hat_Id");
            entity.Property(e => e.LevelId).HasColumnName("Level_Id");
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Accessory).WithMany(p => p.PetAccessories)
                .HasForeignKey(d => d.AccessoryId)
                .HasConstraintName("Pets_Accessory_Id");

            entity.HasOne(d => d.Body).WithMany(p => p.Pets)
                .HasForeignKey(d => d.BodyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pets_Body_Id");

            entity.HasOne(d => d.Eyes).WithMany(p => p.Pets)
                .HasForeignKey(d => d.EyesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pets_Eyes_Id");

            entity.HasOne(d => d.Hat).WithMany(p => p.PetHats)
                .HasForeignKey(d => d.HatId)
                .HasConstraintName("Pets_Hat_Id");

            entity.HasOne(d => d.Level).WithMany(p => p.Pets)
                .HasForeignKey(d => d.LevelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pets_Level");

            entity.HasOne(d => d.User).WithMany(p => p.Pets)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Pets_User_Id");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Post_pkey");

            entity.ToTable("Post");

            entity.Property(e => e.CreatedAt).HasColumnName("Created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("Deleted_at");
            entity.Property(e => e.TagId).HasColumnName("Tag_Id");
            entity.Property(e => e.Title)
                .HasColumnType("character varying")
                .HasColumnName("TItle");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Tag).WithMany(p => p.Posts)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Post_Tag");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Post_User");
        });

        modelBuilder.Entity<PostBlock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Post_block _pkey");

            entity.ToTable("Post_block ");

            entity.Property(e => e.ContentTypeId).HasColumnName("ContentType_Id");
            entity.Property(e => e.PostId).HasColumnName("Post_Id");

            entity.HasOne(d => d.ContentType).WithMany(p => p.PostBlocks)
                .HasForeignKey(d => d.ContentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PostBlock_ContentType");

            entity.HasOne(d => d.Post).WithMany(p => p.PostBlocks)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PostBlock_Post");
        });

        modelBuilder.Entity<PostComment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Post_comments _pkey");

            entity.ToTable("Post_comments ");

            entity.Property(e => e.CreatedAt).HasColumnName("Created_at");
            entity.Property(e => e.PostId).HasColumnName("Post_Id");
            entity.Property(e => e.UpdatedAt).HasColumnName("Updated_at");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.Post).WithMany(p => p.PostComments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PostComment_Post");

            entity.HasOne(d => d.User).WithMany(p => p.PostComments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Post_comments_User");
        });

        modelBuilder.Entity<PostRating>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Post_rating _pkey");

            entity.ToTable("Post_rating ");

            entity.Property(e => e.PostId).HasColumnName("Post_Id");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.Post).WithMany(p => p.PostRatings)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("Post_rating_Post");

            entity.HasOne(d => d.User).WithMany(p => p.PostRatings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Post_rating_User");
        });

        modelBuilder.Entity<PostView>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Post_view_pkey");

            entity.ToTable("Post_view");

            entity.Property(e => e.CreatedAt).HasColumnName("Created_at");
            entity.Property(e => e.PostId).HasColumnName("Post_Id");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.Post).WithMany(p => p.PostViews)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PostView_Post");

            entity.HasOne(d => d.User).WithMany(p => p.PostViews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Post_View_User");
        });

        modelBuilder.Entity<Rarity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Rarity_pkey");

            entity.ToTable("Rarity");

            entity.Property(e => e.Rarity1)
                .HasMaxLength(45)
                .HasColumnName("Rarity");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Role_pkey");

            entity.ToTable("Role");

            entity.Property(e => e.Role1)
                .HasMaxLength(100)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tags_pkey");

            entity.Property(e => e.Tag1)
                .HasMaxLength(50)
                .HasColumnName("Tag");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Tasks_pkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.VkId, "vkid");

            entity.Property(e => e.BackgroundId).HasColumnName("Background_Id");
            entity.Property(e => e.FatesCount).HasColumnName("Fates_Count");
            entity.Property(e => e.PrimogemsCount).HasColumnName("Primogems_Count");
            entity.Property(e => e.VkId).HasColumnName("Vk_Id");
            entity.Property(e => e.WonBattlesCount).HasColumnName("WonBattles_Count");

            entity.HasOne(d => d.Background).WithMany(p => p.Users)
                .HasForeignKey(d => d.BackgroundId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_Background");
        });

        modelBuilder.Entity<UserArtifact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_Artifact_pkey");

            entity.ToTable("User_Artifact");

            entity.Property(e => e.ArtifactId).HasColumnName("Artifact_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Artifact).WithMany(p => p.UserArtifacts)
                .HasForeignKey(d => d.ArtifactId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_Artifact_Artifact");

            entity.HasOne(d => d.User).WithMany(p => p.UserArtifacts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_Artifact_User");
        });

        modelBuilder.Entity<UserBackground>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_Background_pkey");

            entity.ToTable("User_Background");

            entity.Property(e => e.BackgroundId).HasColumnName("Background_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Background).WithMany(p => p.UserBackgrounds)
                .HasForeignKey(d => d.BackgroundId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_Background_Background");

            entity.HasOne(d => d.User).WithMany(p => p.UserBackgrounds)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_Background_User");
        });

        modelBuilder.Entity<UserBattle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("UserBattles_pkey");

            entity.Property(e => e.FirstPlayerId).HasColumnName("FirstPlayer_Id");
            entity.Property(e => e.SecondPlayerId).HasColumnName("SecondPlayer_Id");
            entity.Property(e => e.WinnerId).HasColumnName("Winner_Id");

            entity.HasOne(d => d.FirstPlayer).WithMany(p => p.UserBattleFirstPlayers)
                .HasForeignKey(d => d.FirstPlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserBattles_FirstPlayer");

            entity.HasOne(d => d.SecondPlayer).WithMany(p => p.UserBattleSecondPlayers)
                .HasForeignKey(d => d.SecondPlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserBattles_SecondPlayer");

            entity.HasOne(d => d.Winner).WithMany(p => p.UserBattleWinners)
                .HasForeignKey(d => d.WinnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserBattles_Winner");
        });

        modelBuilder.Entity<UserClan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName(" User_Clan_pkey");

            entity.ToTable(" User_Clan");

            entity.Property(e => e.ClanId).HasColumnName("Clan_Id");
            entity.Property(e => e.RoleId).HasColumnName("Role_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Clan).WithMany(p => p.UserClans)
                .HasForeignKey(d => d.ClanId)
                .HasConstraintName("User_Clan_Clan");

            entity.HasOne(d => d.Role).WithMany(p => p.UserClans)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_Clan_Role");

            entity.HasOne(d => d.User).WithMany(p => p.UserClans)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_Clan_User");
        });

        modelBuilder.Entity<UserFood>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_Food_pkey");

            entity.ToTable("User_Food");

            entity.Property(e => e.FoodId).HasColumnName("Food_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Food).WithMany(p => p.UserFoods)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_Food_Food");

            entity.HasOne(d => d.User).WithMany(p => p.UserFoods)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_Food_User");
        });

        modelBuilder.Entity<UserTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_Tasks_pkey");

            entity.ToTable("User_Tasks");

            entity.Property(e => e.TaskId).HasColumnName("Task_Id");
            entity.Property(e => e.UserId).HasColumnName("User_Id");

            entity.HasOne(d => d.Task).WithMany(p => p.UserTasks)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_Tasks _Task");

            entity.HasOne(d => d.User).WithMany(p => p.UserTasks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("User_Tasks_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
