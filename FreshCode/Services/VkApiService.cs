using Newtonsoft.Json;

namespace FreshCode.Services
{
    public class VkApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _accessToken;
        private readonly string _apiVersion;

        public VkApiService(HttpClient httpClient, string accessToken, string apiVersion = "5.131")
        {
            _httpClient = httpClient;
            _accessToken = accessToken;
            _apiVersion = apiVersion;
        }

        //TODO: изменить _accessToken
        public async Task<List<long>> GetUserFriendsIds(string userId)
        {
            var url = $"https://api.vk.com/method/friends.get?user_ids={userId}&access_token={_accessToken}&v={_apiVersion}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var userInfo = JsonConvert.DeserializeObject<List<long>>(content);
            return userInfo;
        }
    }
}
