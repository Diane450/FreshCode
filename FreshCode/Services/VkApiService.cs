using FreshCode.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace FreshCode.Services
{
    public class VkApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _accessToken;
        private readonly string _apiVersion;

        public VkApiService(HttpClient httpClient, IOptions<VkApiSettings> options)
        {
            _httpClient = httpClient;
            var settings = options.Value;
            _accessToken = settings.AccessToken;
            _apiVersion = settings.ApiVersion;
        }

        public async Task<List<long>> GetUserFriendsIds(string userId)
        {
            var url = $"friends.get?user_ids={userId}&v={_apiVersion}&access_token={_accessToken}";

            var response = await _httpClient.GetAsync(_httpClient.BaseAddress + url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<List<long>>(content);
            return userInfo;
        }
    }
}