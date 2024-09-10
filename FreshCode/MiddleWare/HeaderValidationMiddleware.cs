using FreshCode.Services;
using Microsoft.Net.Http.Headers;
using System.Text;

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
            try
            {
                if (VkLaunchParamsService.VerifySignature(context.Request.Headers))
                {
                    await _next(context);
                    return;
                }
                context.Response.StatusCode = 401; // Unauthorized
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 401;
                var jsonString = "{\"error\": \"Authorization header is missing or empty\"}";

                context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
                await context.Response.WriteAsync(jsonString, Encoding.UTF8);
            }
        }
    }
}
