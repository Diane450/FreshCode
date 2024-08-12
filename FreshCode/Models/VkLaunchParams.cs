namespace FreshCode.Models
{
    public class VkLaunchParams
    {
        public readonly int vk_user_id;
        public readonly int vk_app_id;
        public readonly int? vk_chat_id;
        public readonly bool vk_is_app_user;
        public readonly bool vk_are_notifications_enabled;
        public readonly string vk_language;
        public readonly string vk_ref;
        public readonly string? vk_access_token_settings;
        public readonly int? vk_group_id;
        public readonly string? vk_viewer_group_role;
        public readonly string vk_platform;
        public readonly bool vk_is_favorite;
        public readonly int vk_ts;
        public readonly bool vk_is_recommended;
        public readonly int? vk_profile_id;
        public readonly bool vk_has_profile_button;
        public readonly int? vk_testing_group_id;
        public readonly string sign;
        public readonly bool odr_enabled;
        
        public VkLaunchParams(Dictionary<string, string> data)
        {
            vk_user_id = data.ContainsKey("vk_user_id") ?  Convert.ToInt32(data["vk_user_id"]) : 0;
            vk_app_id = data.ContainsKey("vk_app_id") ? Convert.ToInt32(data["vk_app_id"]) : 0;
            vk_chat_id = data.ContainsKey("vk_chat_id") ? Convert.ToInt32(data["vk_chat_id"]) : null;
            vk_is_app_user = data.ContainsKey("vk_is_app_user") ? Convert.ToBoolean(data["vk_is_app_user"]) : false;
            vk_are_notifications_enabled = data.ContainsKey("vk_are_notifications_enabled") ? Convert.ToBoolean(data["vk_are_notifications_enabled"]) : false;
            vk_language = data.ContainsKey("vk_language") ? (string)data["vk_language"] : "undefined";
            vk_ref = data.ContainsKey("vk_ref") ? (string)data["vk_ref"] : "other";
            vk_access_token_settings = data.ContainsKey("vk_access_token_settings") ? (string)data["vk_access_token_settings"] : string.Empty;
            vk_group_id = data.ContainsKey("vk_group_id") ? Convert.ToInt32(data["vk_group_id"]) : null;
            vk_viewer_group_role = data.ContainsKey("vk_viewer_group_role") ? (string)data["vk_viewer_group_role"] : null;
            vk_platform = data.ContainsKey("vk_platform") ? (string)data["vk_platform"] : "undefined";
            vk_is_favorite = data.ContainsKey("vk_is_favorite") ? Convert.ToBoolean(data["vk_is_favorite"]) : false;
            vk_ts = data.ContainsKey("vk_ts") ? Convert.ToInt32(data["vk_ts"]) : 0;
            vk_is_recommended = data.ContainsKey("vk_is_recommended") ? Convert.ToBoolean(data["vk_is_recommended"]) : false;
            vk_profile_id = data.ContainsKey("vk_profile_id") ? Convert.ToInt32(data["vk_profile_id"]) : null;
            vk_has_profile_button = data.ContainsKey("vk_has_profile_button") ? Convert.ToBoolean(data["vk_has_profile_button"]) : false;
            vk_testing_group_id = data.ContainsKey("vk_testing_group_id") ? Convert.ToInt32(data["vk_testing_group_id"]) : null;
            sign = data.ContainsKey("sign") ? (string)data["sign"] : string.Empty;
            odr_enabled = data.ContainsKey("odr_enabled") ? Convert.ToBoolean(data["odr_enabled"]) : false;
        }
    }
}
