using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    public class BaseController : Controller
    {
        protected long GetUserId(HttpContext context)
        {
            return (long)context.Items["userId"]!;
        }

        protected long GetVkId(HttpContext context)
        {
            return (long)context.Items["vk_user_id"]!;
        }

    }
}
