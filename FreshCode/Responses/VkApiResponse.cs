namespace FreshCode.Responses
{
    using Newtonsoft.Json;

    public class VkApiResponse<T>
    {
        [JsonProperty("response")]
        public List<T> Response { get; set; } = new List<T>();
    }
}
