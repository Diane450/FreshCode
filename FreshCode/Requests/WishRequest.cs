namespace FreshCode.Requests
{
    /// <summary>
    /// Запрос на крутку
    /// </summary>
    public class WishRequest
    {
        /// <summary>
        /// Id баннера
        /// </summary>
        public long BannerId { get; set; }
        /// <summary>
        /// Кол-во круток за один раз
        /// </summary>
        public int WishAmount { get; set; }
    }
}
