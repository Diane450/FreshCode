using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FreshCode.Services
{
    public class VkLaunchParamsService
    {
        //private static string url = "vk_user_id=494075&vk_app_id=6736218&vk_is_app_user=1&vk_are_notifications_enabled=1&vk_language=ru&vk_access_token_settings=&vk_platform=android&sign=htQFduJpLxz7ribXRZpDFUH-XEUhC9rBPTJkjUFEkRA";
        private static string clientSecret = "wvl68m4dR1UpLrVRli";

        public static bool VerifySignature(IHeaderDictionary header)
        {
            //TODO:проверка header на null
            string url = Decode(header.Authorization);
            var queryParams = GetQueryParams(url);
            var checkString = string.Join("&", queryParams
            .Where(entry => entry.Key.StartsWith("vk_"))
            .OrderBy(entry => entry.Key)
            .Select(entry => $"{HttpUtility.UrlEncode(entry.Key)}={HttpUtility.UrlEncode(entry.Value)}"));

            var sign = GetHashCode(checkString, clientSecret);
            return sign == queryParams["sign"] /*&& !IsExpired(queryParams)*/;
        }

        public static async Task<string?> GetParamValueAsync(IHeaderDictionary header, string key)
        {
            string url = Decode(header.Authorization);
            var queryParams = await Task.Run(() => GetQueryParams(url));
            return queryParams.ContainsKey(key) ? queryParams[key] : null;
        }

        private static Dictionary<string, string> GetQueryParams(string url)
        {
            Dictionary<string, string> result = [];

            var query = url.TrimStart('?');

            string[] pairs = query.Split('&');
            foreach (var pair in pairs)
            {
                int equalsCharIndex = pair.IndexOf('=');
                string key = equalsCharIndex > 0 ? (pair.Substring(0, pair.Length - (pair.Length - equalsCharIndex))) : pair;
                string? value = equalsCharIndex > 0 && pair.Length > equalsCharIndex + 1 ? (pair.Substring(equalsCharIndex + 1)) : null;
                result.Add(key, value);
            }
            return result;
        }

        private static string Decode(string value)
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

        private static string Encode(string value)
        {
            string encodedString = String.Empty;
            try
            {
                byte[] bytesToEncode = Encoding.UTF8.GetBytes(value);
                encodedString = Convert.ToBase64String(bytesToEncode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return encodedString;
        }

        private static string GetHashCode(string data, string key)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hmacData = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return Convert.ToBase64String(hmacData).TrimEnd('=').Replace('+', '-').Replace('/', '_');
            }
        }

        private static bool IsExpired(Dictionary<string, string> queryParams)
        {
            int hour = 3600;
            int vk_ts = Convert.ToInt32(queryParams.ContainsKey("vk_ts") ? queryParams["vk_ts"] : 0);
            DateTime dateTime = DateTime.Now;
            int now = (int)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            return Convert.ToInt32(now - vk_ts) >= hour;
        }
    }
}
