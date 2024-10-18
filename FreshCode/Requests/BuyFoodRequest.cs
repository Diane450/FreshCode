namespace FreshCode.Requests
{
    /// <summary>
    /// Запрос на покупку артефакта
    /// </summary>
    public class BuyFoodRequest
    {
        /// <summary>
        /// Id покупаемой еды
        /// </summary>
        public long FoodId { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// Кол-во
        /// </summary>
        public int Count { get; set; }
    }
}
