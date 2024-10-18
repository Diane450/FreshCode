namespace FreshCode.ModelsDTO
{
    /// <summary>
    /// DTO Задний фон
    /// </summary>
    public class BackgroundDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Значение в тайловой карте по Х
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Значение в тайловой карте по У
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Цена в магазине
        /// </summary>
        public int Price { get; set; }
    }
}
