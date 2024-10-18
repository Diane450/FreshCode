namespace FreshCode.Responses
{
    /// <summary>
    /// Ответ текуще статы питомца вместе с бонусами
    /// </summary>
    public class PetStatResponse
    {
        /// <summary>
        /// крит урон
        /// </summary>
        public decimal CriticalDamage { get; set; }
        /// <summary>
        /// крит шанс
        /// </summary>
        public decimal CriticalChance { get; set; }
        /// <summary>
        /// здоровье
        /// </summary>
        public int Health { get; set; }
        /// <summary>
        /// сила
        /// </summary>
        public int Strength { get; set; }
        /// <summary>
        /// защита
        /// </summary>
        public int Defence { get; set; }
        /// <summary>
        /// уровень сна
        /// </summary>
        public int SleepNeed { get; set; }
        /// <summary>
        /// уровень голода
        /// </summary>
        public int FeedNeed { get; set; }
        /// <summary>
        /// средняя сила
        /// </summary>
        public decimal AveragePower { get; set; }
    }
}
