using FreshCode.DbModels;
using FreshCode.Enums;
using FreshCode.ModelsDTO;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FreshCode.Requests
{
    public class IncreaseStatRequest
    {
        public CharacteristicType Characteristic { get; set; }

        public long PetId { get; set; }
    }
}
