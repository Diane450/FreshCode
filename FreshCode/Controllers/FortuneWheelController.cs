﻿using FreshCode.DbModels;
using FreshCode.ModelsDTO;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("fortune-wheel")]
    public class FortuneWheelController(FortuneWheelUseCase fortuneWheelUseCase) : BaseController
    {
        private readonly FortuneWheelUseCase _fortuneWheelUseCase = fortuneWheelUseCase;

        [HttpGet("get-value")]
        public void GetValue()
        {
            long userId = GetUserId(HttpContext);
            return _fortuneWheelUseCase.SpinFortuneWheel(userId);
        }
    }
}
