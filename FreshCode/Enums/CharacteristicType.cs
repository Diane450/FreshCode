using System.ComponentModel;
using System.Text.Json.Serialization;

namespace FreshCode.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum CharacteristicType
    {
        [Description("Здоровье")]
        Health,
        [Description("Сила")]
        Strength,
        [Description("Защита")]
        Defence,
        [Description("Критический урон")]
        CriticalDamage,
        [Description("Критический шанс")]
        CriticalChance,
        [Description("Сон")]
        SleepNeed,
        [Description("Питание")]
        FeedNeed
    }
}
