using FreshCode.DbModels;
using FreshCode.Extensions;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.Models;
using FreshCode.ModelsDTO;
using FreshCode.Repositories;
using FreshCode.Services;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FreshCode.UseCases
{
    public class UserUseCase(IUserRepository userRepository,
        IClanRepository clanRepository,
        VkApiService vkApiService,
        IBaseRepository baseRepository,
        IBackgroundRepository backgroundRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IClanRepository _clanRepository = clanRepository;
        private readonly VkApiService _vkApiService = vkApiService;
        private readonly IBaseRepository _baseRepository = baseRepository;
        private readonly IBackgroundRepository _backgroundRepository = backgroundRepository;

        public async Task<UserDTO> GetUserGameInfo(long userId)
        {
            return await _userRepository.GetUserGameInfo(userId);
        }

        public async Task<long> GetUserIdByVkId(long vk_user_id)
        {
            try
            {
                return await _userRepository.GetUserIdByVkId(vk_user_id);
            }
            catch (ArgumentException ex)
            {
                User user = new User()
                {
                    Money = 0,
                    StatPoints = 0,
                    BackgroundId = 6,
                    WonBattlesCount = 0,
                    PrimogemsCount = 0,
                    FatesCount = 0,
                    VkId = (int)vk_user_id
                };
                await _baseRepository.AddAsync(user);
                await _baseRepository.SaveChangesAsync();
                return user.Id;
            }
        }

        public async Task<List<TaskDTO>> GetUserTasks(long userId)
        {
            var tasks = _userRepository.GetUserTasks(userId);
            var tasksDTO = tasks.Select(t => TaskMapper.ToDTO(t)).ToList();
            return tasksDTO;
        }

        public async Task<List<ArtifactHistoryDTO>> GetArtifactHistory(long userId, long bannerId)
        {
            return _userRepository.GetArtifactHistory(userId, bannerId)
                .Select(ah => ArtifactHistoryMapper.ToDTO(ah)).ToList();
        }

        public async Task<List<UserFoodDTO>> GetUserFood(long userId)
        {
            var food = _userRepository.GetUserFood(userId).ToList();

            return UserFoodMapper.ToDTO(food);
        }

        public async Task<List<ArtifactDTO>> GetUserArtifact(long userId)
        {
            return await _userRepository.GetUserArtifact(userId);
        }

        public async Task<List<BackgroundDTO>> GetUserBackgrounds(long userId)
        {
            var backgrounds = _userRepository.GetUserBackgrounds(userId);
            var bacgroundsList = await backgrounds.Select(b => BackgroundMapper.ToDTO(b.Background)).ToListAsync();
            return bacgroundsList;
        }

        public async Task<BackgroundDTO> SetBackground(long backgroundId, long userId)
        {
            User user = await _userRepository.GetUserById(userId);

            UserBackground? userBackground = _userRepository.GetUserBackgrounds(userId)
                .FirstOrDefault(ub => ub.BackgroundId == backgroundId);

            if (userBackground is null)
            {
                throw new ArgumentException("У пользователя нет выбранного заднего фона");
            }

            user.BackgroundId = backgroundId;
            await _baseRepository.SaveChangesAsync();
            return BackgroundMapper.ToDTO(await _backgroundRepository.GetBackgroundById(backgroundId));
        }

        public async Task<PagedList<UserRatingTableDTO>> GetAllUsersRatingTable(QueryParameters queryParameters)
        {
            IQueryable<User> users = _userRepository.GetAllUsers().OrderByDescending(u => u.WonBattlesCount);

            //users = users.Sort(queryParameters.SortBy, queryParameters.SortDescending);

            //users = users.Filter(queryParameters.FilterBy, queryParameters.FilterValue);

            int totalCount = await users.CountAsync();

            users = users.Paginate(queryParameters.Page, queryParameters.PageSize);
            var usersDTO = users.Select(u => UserMapper.ToRatingTableDTO(u)).ToList();
            // Для каждого пользователя запросим данные из VK API

            var idsList = String.Join(",", usersDTO.Select(u => u.Id).ToList());

            var vkUsersInfo = await _vkApiService.GetVkUsersInfo(idsList);

            foreach (var vkUser in vkUsersInfo)
            {
                var userDTO = usersDTO.FirstOrDefault(u => u.Id == vkUser.Id);
                if (userDTO != null)
                {
                    userDTO.FirstName = vkUser.FirstName;
                    userDTO.LastName = vkUser.LastName;
                    userDTO.Photo50 = vkUser.Photo50;
                }
            }
            return new PagedList<UserRatingTableDTO>(usersDTO, queryParameters.Page, queryParameters.PageSize, totalCount);
        }

        public async Task<List<ClanRatingTableDTO>> GetClanRatingTable(long userId)
        {
            IQueryable<User> ClanUsers = await _userRepository.GetClanUsers(userId);

            List<ClanRatingTableDTO> clans = await _clanRepository.GetClanRatingTable();
            return [.. clans.OrderByDescending(c => c.WonBattlesCount)];
        }

        public async Task<PagedList<UserRatingTableDTO>> GetFriendsRatingTable(QueryParameters queryParameters, long vk_user_id)
        {
            IQueryable<User> users = _userRepository.GetAllUsers();

            var friendsIds = await _vkApiService.GetVkFriends(vk_user_id);

            friendsIds.Add(vk_user_id);

            var filteredUsers = users.Where(user => friendsIds.Contains(user.VkId));

            int totalCount = await filteredUsers.CountAsync();

            filteredUsers = filteredUsers.Paginate(queryParameters.Page, queryParameters.PageSize);
            filteredUsers = filteredUsers.OrderByDescending(u => u.WonBattlesCount);
            var usersDTO = filteredUsers.Select(u => UserMapper.ToRatingTableDTO(u)).ToList();
            var idsList = String.Join(",", usersDTO.Select(u => u.Id).ToList());

            var vkUsersInfo = await _vkApiService.GetVkUsersInfo(idsList);

            foreach (var vkUser in vkUsersInfo)
            {
                var userDTO = usersDTO.FirstOrDefault(u => u.Id == vkUser.Id);
                if (userDTO != null)
                {
                    userDTO.FirstName = vkUser.FirstName;
                    userDTO.LastName = vkUser.LastName;
                    userDTO.Photo50 = vkUser.Photo50;
                }
            }
            return new PagedList<UserRatingTableDTO>(usersDTO, queryParameters.Page, queryParameters.PageSize, totalCount);
        }
    }
}
