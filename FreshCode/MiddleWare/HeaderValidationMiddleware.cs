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
        private HttpContext _httpContext { get; set; }

        private IMiddleWare _middleWare;

        public HeaderValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, UserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
            _httpContext = context;
            
            try
            {
                var platform = _httpContext.Request.Headers["Platform"].FirstOrDefault();

                _middleWare = MiddlewareFabric.Create(platform);
                
                VerifySignature(_middleWare, _httpContext);

                var userId = await GetUserId();

                _httpContext.Items["userId"] = userId;

                await _next(_httpContext);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 401;
                var jsonString = $"error: {ex.Message}";

                context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
                await context.Response.WriteAsync(jsonString, Encoding.UTF8);
            }
        }

        private async Task<long> GetUserId()
        {
            if (!_httpContext.Request.Cookies.ContainsKey("userId"))
            {
                long id = await GetUserId(_middleWare.QueryParams["vk_user_id"]);
                await SetUserIdCookie(_httpContext, id);
                return id;
            }
            return Convert.ToInt64(_httpContext.Request.Cookies["userId"]);
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
            _httpContext.Response.Cookies.Append("userId", innerId.ToString());
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
