using FreshCode.DbModels;
using FreshCode.Enums;
using FreshCode.ModelsDTO;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FreshCode.Requests
{
    /// <summary>
    /// запрос на повышение конкретного стата
    /// </summary>
    public class IncreaseStatRequest
    {
        /// <summary>
        /// Характеристика
        /// </summary>
        public CharacteristicType Characteristic { get; set; }
        /// <summary>
        /// Id питомца
        /// </summary>
        public long PetId { get; set; }
    }
}
