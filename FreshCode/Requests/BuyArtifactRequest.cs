namespace FreshCode.Requests
{
    /// <summary>
    /// Запрос на покупку артефакта
    /// </summary>
    public class BuyArtifactRequest
    {
        /// <summary>
        /// Id артефакта
        /// </summary>
        public long ArtifactId { get; set; }
        /// <summary>
        /// Цена
        /// </summary>
        public int Price { get; set; }
    }
}
