namespace FreshCode.Requests
{
    /// <summary>
    /// Запрос на покупку заднего фона
    /// </summary>
    public class BuyBackgroundRequest
    {
        /// <summary>
        /// Id заднего фона
        /// </summary>
        public long BackgroundId { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public int Price { get; set; }

    }
}
