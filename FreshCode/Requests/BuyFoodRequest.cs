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
        /// Кол-во
        /// </summary>
        public int Count { get; set; }
    }
}
