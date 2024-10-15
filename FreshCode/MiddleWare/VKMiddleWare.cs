
using FreshCode.Interfaces;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FreshCode.MiddleWare
{
    public class VKMiddleWare(UserUseCase userUseCase) : IMiddleWare
    {
        private readonly static string  clientSecret = "wvl68m4dR1UpLrVRli";
        
        private readonly UserUseCase _userUseCase = userUseCase;

        public Dictionary<string, string> QueryParams { get; set; } = [];
        
        public bool VerifySignature(IHeaderDictionary header)
        {
            ValidateAuthorizationHeader(header.Authorization);

            string url = Decode(header.Authorization!);
            var queryParams = GetQueryParams(url);
            var checkString = string.Join("&", queryParams
            .Where(entry => entry.Key.StartsWith("vk_"))
            .OrderBy(entry => entry.Key)
            .Select(entry => $"{HttpUtility.UrlEncode(entry.Key)}={HttpUtility.UrlEncode(entry.Value)}"));

            var sign = GetHashCode(checkString, clientSecret);
            return sign == queryParams["sign"] /*&& !IsExpired(queryParams)*/;
        }

        public async Task<string?> GetParamValueAsync(IHeaderDictionary header, string key)
        {
            string url = Decode(header.Authorization!);
            var queryParams = await Task.Run(() => GetQueryParams(url));
            return queryParams.ContainsKey(key) ? queryParams[key] : throw new ArgumentException("key is not found");
        }

        private void ValidateAuthorizationHeader(StringValues header)
        {
            if (StringValues.IsNullOrEmpty(header))
            {
                throw new ArgumentNullException(nameof(header), "Authorization header is missing or empty");
            }
        }

        private Dictionary<string, string> GetQueryParams(string url)
        {
            var query = url.TrimStart('?');

            string[] pairs = query.Split('&');
            foreach (var pair in pairs)
            {
                int equalsCharIndex = pair.IndexOf('=');
                string key = equalsCharIndex > 0 ? (pair[..^(pair.Length - equalsCharIndex)]) : pair;
                string? value = equalsCharIndex > 0 && pair.Length > equalsCharIndex + 1 ? (pair[(equalsCharIndex + 1)..]) : null;
                QueryParams.Add(key, value);
            }
            return QueryParams;
        }

        private string Decode(string value)
        {
            string decodedStr = String.Empty;
            try
            {
                value = value.Replace("VK", "");
                byte[] decodedBytes = Convert.FromBase64String(value);
                decodedStr = Encoding.UTF8.GetString(decodedBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return decodedStr;
        }

        private string GetHashCode(string data, string key)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key));
            byte[] hmacData = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return Convert.ToBase64String(hmacData).TrimEnd('=').Replace('+', '-').Replace('/', '_');
        }

        private bool IsExpired(Dictionary<string, string> queryParams)
        {
            int hour = 3600;
            int vk_ts = Convert.ToInt32(queryParams.TryGetValue("vk_ts", out string? value) ? value : 0);
            DateTime dateTime = DateTime.Now;
            int now = (int)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return Convert.ToInt32(now - vk_ts) >= hour;
        }

        public async Task<long> GetInnerId(HttpContext context)
        {
            long vk_user_id = Convert.ToInt64(QueryParams["vk_user_id"]);
            return await _userUseCase.GetUserIdByVkId(vk_user_id);
        }

        public long GetVkId()
        {
            return Convert.ToInt64(QueryParams["vk_user_id"]);
        }
    }
}
