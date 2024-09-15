using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    public class BaseController : Controller
    {
        protected long GetUserId(HttpContext context)
        {
            return (long)context.Items["userId"]!;
        }
    }
}
