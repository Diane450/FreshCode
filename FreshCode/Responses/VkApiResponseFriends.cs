using Newtonsoft.Json;

namespace FreshCode.Responses
{
    public class VkApiResponseFriends<T>
    {
        [JsonProperty("response")]
        public T Response { get; set; }
    }
}
