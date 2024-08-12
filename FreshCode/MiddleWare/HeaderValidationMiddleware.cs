using FreshCode.Services;

namespace FreshCode.MiddleWare
{
    public class HeaderValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public HeaderValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (VkLaunchParamsService.VerifySignature(context.Request.Headers))
            {
                await _next(context);
                return;
            }
            context.Response.StatusCode = 401; // Unauthorized
        }
    }
}
