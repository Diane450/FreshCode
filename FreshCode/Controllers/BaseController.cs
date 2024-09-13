using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly UserUseCase _userUseCase;

        public BaseController(UserUseCase userUseCase)
        {
            _userUseCase = userUseCase;
        }
        protected string GetUserIdFromCookies()
        {
            return null;
        }
    }
}
