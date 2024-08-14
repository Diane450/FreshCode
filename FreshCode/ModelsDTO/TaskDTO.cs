namespace FreshCode.ModelsDTO
{
    public class TaskDTO
    {
        public long Id { get; set; }

        public string Descryption { get; set; } = null!;

        public int MoneyReward { get; set; }

        public int PointsReward { get; set; }

        public int StatPointsReward { get; set; }

        public int PrimogemsReward { get; set; }

        public bool IsCompleted { get; set; }

    }
}
