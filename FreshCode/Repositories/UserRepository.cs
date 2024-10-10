﻿using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FreshCode.Repositories
{
    public class UserRepository(FreshCodeContext dbContext) : IUserRepository
    {
        private readonly FreshCodeContext _dbContext = dbContext;

        public async Task<long> GetUserIdByVkId(long vk_user_id)
        {
            try
            {
                return await _dbContext.Users
                    .Where(u => u.VkId == Convert.ToInt64(vk_user_id))
                    .Select(u => u.Id)
                    .FirstAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException("Пользователь не найден");
            }
        }

        public async Task<UserDTO> GetUserGameInfo(long userId)
        {
            return await _dbContext.Users
                .Where(u => u.Id == userId)
                .Include(u => u.Background)
                .Select(user => UserMapper.ToDTO(user))
                .FirstAsync();
        }

        public async Task<List<TaskDTO>> GetUserTasks(long userId)
        {
            return await _dbContext.UserTasks
                .Where(ut => ut.UserId == userId)
                .Include(ut => ut.Task)
                .Select(task => TaskMapper.ToDTO(task))
                .ToListAsync();
        }

        public IQueryable<ArtifactHistory> GetArtifactHistory(long userId, long bannerId)
        {
            return _dbContext.ArtifactHistories
                .Where(ah => ah.UserId == userId && ah.BannerId == bannerId)
                .Include(ah => ah.Artifact)
                .ThenInclude(a => a.ArtifactType)
                .Include(ah => ah.Artifact)
                .ThenInclude(a => a.Rarity);
        }

        public IQueryable<UserFood> GetUserFood(long userId)
        {
            return _dbContext.UserFoods
                .Where(uf => uf.UserId == userId)
                .Include(uf => uf.Food)
                .ThenInclude(f => f.FoodBonuses)
                .ThenInclude(fb => fb.Bonus)
                .ThenInclude(b => b.Characteristic)
                .Include(uf => uf.Food)
                .ThenInclude(f => f.FoodBonuses)
                .ThenInclude(fb => fb.Bonus)
                .ThenInclude(b => b.Type);
        }

        public async Task<List<ArtifactDTO>> GetUserArtifact(long userId)
        {
            return await _dbContext.UserArtifacts
                .Where(ua => ua.UserId == userId)
                .Include(ah => ah.Artifact)
                .ThenInclude(a => a.ArtifactType)
                .Include(ah => ah.Artifact)
                .ThenInclude(a => a.Rarity)
                .Include(ah => ah.Artifact)
                .ThenInclude(a => a.ArtifactBonuses)
                .ThenInclude(ab => ab.Bonus)
                .ThenInclude(b => b.Characteristic)
                .Include(ah => ah.Artifact)
                .ThenInclude(a => a.ArtifactBonuses)
                .ThenInclude(ab => ab.Bonus)
                .ThenInclude(b => b.Type)
                .Select(artifactHistory => ArtifactMapper.ToDTO(artifactHistory.Artifact))
                .ToListAsync();
        }

        public async Task<List<BackgroundDTO>> GetUserBackgrounds(long userId)
        {
            return await _dbContext.UserBackgrounds
                .Where(ub => ub.UserId == userId)
                .Include(ub => ub.Background)
                .Select(userBackground => BackgroundMapper.ToDTO(userBackground.Background))
                .ToListAsync();
        }

        public async Task<User> GetUserByVkId(long vk_user_id)
        {
            try
            {
                return await _dbContext.Users
                    .FirstAsync(u => u.VkId == Convert.ToInt32(vk_user_id));
            }
            catch (Exception)
            {
                throw new ArgumentException("Пользователь не найден");
            }
        }

        public async System.Threading.Tasks.Task CreateNewClan(Clan clan)
        {
            await _dbContext.Clans.AddAsync(clan);
        }

        public async System.Threading.Tasks.Task AddUserClan(UserClan userClan)
        {
            await _dbContext.UserClans.AddAsync(userClan);
        }

        public async Task<Clan> GetClanByUser(long userId)
        {
            var userClan = await _dbContext.UserClans
                .Where(uc => uc.UserId == userId)
                .Include(us => us.Clan)
                .Select(us => us.Clan)
                .FirstOrDefaultAsync();

            if (userClan is null)
            {
                throw new ArgumentException("Клан не найден");
            }

            return userClan;
        }

        public async Task<List<UserRatingTableDTO>> GetAllUsersRatingTable()
        {
            return await _dbContext.Users
                .Select(u => UserMapper.ToRatingTableDTO(u))
                .ToListAsync();
        }

        //TODO:test
        public async Task<List<UserRatingTableDTO>> GetFriendsRatingTable(List<long> friendsIds)
        {
            List<UserRatingTableDTO> userFriendsRating = [];

            foreach (long id in friendsIds)
            {
                UserRatingTableDTO userRatingTable = await _dbContext.Users
                    .Where(u => u.VkId == id)
                    .Select(u => UserMapper.ToRatingTableDTO(u))
                    .FirstAsync();
            }
            return userFriendsRating.OrderByDescending(ufr => ufr.WonBattlesCount).ToList();
        }

        public Task<UserFood> GetUserFoodByFoodId(long foodId)
        {
            return _dbContext.UserFoods.FirstAsync(uf => uf.FoodId == foodId);
        }

        public async Task<bool> isArtifactAbsent(long artifactId, long userId)
        {
            var count = await _dbContext.UserArtifacts
                .Where(a => a.ArtifactId == artifactId && a.UserId == userId)
                .CountAsync();

            return count != 0;
        }

        public async Task<bool> isBackgroundAbsent(long backgroundId, long userId)
        {
            var count = await _dbContext.UserArtifacts
                .Where(a => a.Id == backgroundId && a.UserId == userId)
                .CountAsync();

            return count != 0;
        }

        public async Task<User> GetUserById(long userId)
        {
            try
            {
                return await _dbContext.Users
                    .Where(u => u.Id == userId)
                    .FirstAsync();
            }
            catch (Exception)
            {
                throw new ArgumentException("Пользователь не найден");
            }
        }

        public IQueryable<User> GetUsersByClanId(long clanId)
        {
            return _dbContext.UserClans
                .Where(uc => uc.ClanId == clanId)
                .Include(c => c.User)
                .Select(c=>c.User);
        }
    }
}
