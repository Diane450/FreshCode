namespace FreshCode.ModelsDTO
{
    public class ClanDTO
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public long MemberCount { get; set; }

        public long WonBattlesCount { get; set; }
    }
}
