﻿using FreshCode.ModelsDTO;
using FreshCode.Services;
using FreshCode.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FreshCode.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class UserController : BaseController
    {

        public UserController(UserUseCase userUseCase):base(userUseCase)
        {

        }

        [HttpGet]
        public async Task <UserDTO> GetUserGameInfo()
        {
            var user_id = GetUserIdFromCookies();
            return await _userUseCase.GetUserGameInfo(user_id);
        }

        [HttpGet]
        public async Task<List<TaskDTO>> GetUserTasks()
        {
            var user_id = Request.Cookies["userId"]!;
            return await _userUseCase.GetUserTasks(user_id);
        }

        [HttpGet]
        public async Task<List<ArtifactHistoryDTO>> GetArtifactHistory()
        {
            var user_id = Request.Cookies["userId"]!;
            return await _userUseCase.GetArtifactHistory(user_id);
        }

        [HttpGet]
        public async Task<List<UserFoodDTO>> GetUserFood()
        {
            var user_id = GetUserIdFromCookies();
            return await _userUseCase.GetUserFood(user_id);
        }

        [HttpGet]
        public async Task<List<ArtifactDTO>> GetUserArtifact()
        {
            var user_id = Request.Cookies["userId"]!;
            return await _userUseCase.GetUserArtifact(user_id);
        }

        [HttpGet]
        public async Task<List<BackgroundDTO>> GetUserBackgrounds()
        {
            var user_id = Request.Cookies["userId"]!;
            return await _userUseCase.GetUserBackgrounds(user_id);
        }
    }
}
