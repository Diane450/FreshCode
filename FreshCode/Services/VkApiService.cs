using FreshCode.ModelsDTO;
using FreshCode.Responses;
using FreshCode.Settings;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<List<UserRatingTableDTO>> GetVkUsersInfo(string vk_user_ids)
        {
            var _accessToken = "0c0ce98c0c0ce98c0c0ce98c930f13194300c0c0c0ce98c6b0ea83be278ceee456f5480";
            var _apiVersion = "5.199";

            // Формируем запрос к VK API для получения информации о нескольких пользователях
            var url = $"/users.get?user_ids={vk_user_ids}&fields=first_name,last_name,photo_100&lang=ru&v={_apiVersion}&access_token={_accessToken}";

            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + url);
            response.EnsureSuccessStatusCode();

            var vkContent = await response.Content.ReadAsStringAsync();
            var vkResponse = JsonConvert.DeserializeObject<VkApiResponse<UserRatingTableDTO>>(vkContent);

            if (vkResponse != null && vkResponse.Response.Any())
            {
                return vkResponse.Response;
            }

            return new List<UserRatingTableDTO>();
        }
    }
}