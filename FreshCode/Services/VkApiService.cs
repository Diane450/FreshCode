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

        public async Task<UserRatingTableDTO> GetVkUserInfo(string vk_user_id)
        {
            var _accessToken = "0c0ce98c0c0ce98c0c0ce98c930f13194300c0c0c0ce98c6b0ea83be278ceee456f5480";
            var _apiVersion = "5.199";

            // Формируем запрос к VK API для получения информации о пользователе
            var url = $"/users.get?user_ids={vk_user_id}&fields=first_name,last_name,photo_100&lang=ru&v={_apiVersion}&access_token={_accessToken}";

            HttpResponseMessage response;
            // Отправляем запрос к VK API
            response = await _httpClient.GetAsync(_httpClient.BaseAddress + url);

            // Проверяем успешен ли запрос
            response.EnsureSuccessStatusCode();

            // Читаем и десериализуем ответ
            var vkContent = await response.Content.ReadAsStringAsync();
            var vkResponse = JsonConvert.DeserializeObject<VkApiResponse<UserRatingTableDTO>>(vkContent);

            // Если данные получены, возвращаем первый объект из списка (так как вернется массив)
            if (vkResponse != null && vkResponse.Response.Any())
            {
                return vkResponse.Response.First();
            }
            else
            {
                throw new HttpRequestException();
            }
        }
    }
}