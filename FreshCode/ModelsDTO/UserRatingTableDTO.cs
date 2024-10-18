using FreshCode.DbModels;
using Newtonsoft.Json;

namespace FreshCode.ModelsDTO
{
    public class UserRatingTableDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        //[JsonProperty("won_battles_count")]
        public int WonBattlesCount { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; } = null!;

        [JsonProperty("first_name")]
        public string FirstName { get; set; } = null!;

        [JsonProperty("photo_50")]
        public string Photo50 { get; set; }
    }
}
