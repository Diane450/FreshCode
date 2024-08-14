namespace FreshCode.ModelsDTO
{
    public class UserDTO
    {
        public long Id { get; set; }

        public int Money { get; set; }

        public int StatPoints { get; set; }

        public BackgroundDTO Background { get; set; } = null!;

        public int WonBattlesCount { get; set; }

        public int FatesCount { get; set; }

        public int PrimogemsCount { get; set; }
    }
}
