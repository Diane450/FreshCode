using FreshCode.DbModels;
using FreshCode.Enums;
using FreshCode.ModelsDTO;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FreshCode.Requests
{
    public class IncreaseStatRequest
    {
        public PetDTO PetDTO { get; set; } = null!;
        public CharacteristicType Characteristic { get; set; }
    }
}
