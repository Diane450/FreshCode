namespace FreshCode.Responses
{
    public class PetStatResponse
    {
        public decimal CriticalDamage { get; set; }
        public decimal CriticalChance { get; set; }
        public int Health { get; set; }
        public int Strength { get; set; }
        public int Defence { get; set; }
        public int SleepNeed { get; set; }
        public int FeedNeed { get; set; }
        public decimal AveragePower { get; set; }
    }
}
