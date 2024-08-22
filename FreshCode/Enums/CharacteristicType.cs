using System.Text.Json.Serialization;

namespace FreshCode.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CharacteristicType
    {
        Health,
        Strength,
        Defence,
        CriticalDamage,
        CriticalChance
    }
}
