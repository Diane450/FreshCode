using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using FreshCode.UseCases;
using FreshCode.Interfaces;
using FreshCode.Hubs;

namespace FreshCode.Filters
{
    public class BattleStateFilter : IActionFilter
    {
        private readonly IUserRepository _userRepository;

        public BattleStateFilter(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            var vk_user_id = Convert.ToString(context.HttpContext.Items["vk_user_id"]);

            if (BattleHub._userConnections.ContainsKey(vk_user_id))
            {
                // Устанавливаем статус-код 403 Forbidden
                context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

                // Отправляем сообщение клиенту
                var result = new ObjectResult(new { message = "Данное действие запрещено во время боя!" })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };

                context.Result = result;
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
