using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using System.Diagnostics.Eventing.Reader;
using System.Net;

namespace FreshCode.Requests
{
    /// <summary>
    /// Запрос на покормить питомца
    /// </summary>
    public class FeedRequest
    {
        /// <summary>
        /// Id питомца
        /// </summary>
        public long PetId { get; set; }
        /// <summary>
        /// Id еды
        /// </summary>
        public long FoodId{ get; set; }
    }
}
