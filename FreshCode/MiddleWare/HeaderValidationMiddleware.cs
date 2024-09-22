﻿using FreshCode.Fabrics;
using FreshCode.Interfaces;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.Net.Http.Headers;
using System.Text;

namespace FreshCode.MiddleWare
{
    public class HeaderValidationMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;
        
        private UserUseCase _userUseCase;
        private HttpContext _httpContext;

        private IMiddleWare _middleWare;

        public async Task InvokeAsync(HttpContext context, UserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
            _httpContext = context;
            
            try
            {
                var platform = _httpContext.Request.Headers["Platform"].FirstOrDefault();

                _middleWare = MiddlewareFabric.Create(platform, _userUseCase);
                
                VerifySignature(_middleWare, _httpContext);

                var userId = await GetUserId();

                _httpContext.Items["userId"] = userId;
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 401;
                var jsonString = $"error: {ex.Message}";

                context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
                await context.Response.WriteAsync(jsonString, Encoding.UTF8);
            }
            await _next(_httpContext);
        }

        private async Task<long> GetUserId()
        {
            if (!_httpContext.Request.Cookies.ContainsKey("userId"))
            {
                long id = await _middleWare.GetInnerId(_httpContext);
                await SetUserIdCookie(id);
                return id;
            }
            return Convert.ToInt64(_httpContext.Request.Cookies["userId"]);
        }


        private Task SetUserIdCookie(long innerId)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // Доступ к cookie только через HTTP
                Secure = _httpContext.Request.IsHttps, // Отправка только по HTTPS
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict, // Политика SameSite
                Expires = DateTime.UtcNow.AddDays(365) // Срок действия cookie
            };
            _httpContext.Response.Cookies.Append("userId", innerId.ToString(), cookieOptions);
            return Task.FromResult(0);
        }

        private void VerifySignature(IMiddleWare middleware, HttpContext context)
        {
            var isVerified = middleware.VerifySignature(context.Request.Headers);
            if (!isVerified)
                throw new Exception("Signature is not valid");
        }
    }
}
