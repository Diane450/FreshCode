namespace FreshCode.ModelsDTO
{
    /// <summary>
    /// DTO для пользователя 
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Деньги
        /// </summary>
        public int Money { get; set; }
        /// <summary>
        /// кол-во очков для прокачки статов
        /// </summary>
        public int StatPoints { get; set; }
        /// <summary>
        /// DTO для заднего фона
        /// </summary>
        public BackgroundDTO Background { get; set; } = null!;
        /// <summary>
        /// кол-во выигранных баттлов
        /// </summary>
        public int WonBattlesCount { get; set; }
        /// <summary>
        /// кол-во круток
        /// </summary>
        public int FatesCount { get; set; }
        /// <summary>
        /// кол-во примогемов
        /// </summary>
        public int PrimogemsCount { get; set; }
    }
}
