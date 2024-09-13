using FreshCode.Fabrics;
using FreshCode.Interfaces;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace FreshCode.MiddleWare
{
    public class HeaderValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private UserUseCase _userUseCase;
        public HttpContext _httpContext { get; set; }


        public HeaderValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
            try
            {
                var platform = context.Request.Headers["Platform"].FirstOrDefault();

                var middleware = MiddlewareFabric.Create(platform);
                
                VerifySignature(middleware, context);

                if (!context.Request.Cookies.ContainsKey("userId"))
                {
                    long id = await GetUserId(middleware.QueryParams["vk_user_id"]);
                    SetUserIdCookie(context, id);
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 401;
                var jsonString = $"error: {ex.Message}";

                context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
                await context.Response.WriteAsync(jsonString, Encoding.UTF8);
            }
        }

        private Task SetUserIdCookie(HttpContext context, long innerId)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // Доступ к cookie только через HTTP
                Secure = _httpContext.Request.IsHttps, // Отправка только по HTTPS
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict, // Политика SameSite
                Expires = DateTime.UtcNow.AddDays(7) // Срок действия cookie
            };
            _httpContext.Response.Cookies.Append("userId", "test");
            return Task.FromResult(0);
        }

        private void VerifySignature(IMiddleWare middleware, HttpContext context)
        {
            var isVerified = middleware.VerifySignature(context.Request.Headers);
            if (!isVerified)
                throw new Exception("Signature is not valid");
        }

        private async Task<long> GetUserId(string vk_user_id)
        {
            return await _userUseCase.GetUserId(vk_user_id);
        }
    }
}
