
using FreshCode.DbModels;
using FreshCode.Extensions;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.Models;
using FreshCode.ModelsDTO;
using FreshCode.Repositories;
using FreshCode.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FreshCode.UseCases
{
    public class ClanUseCase(IUserRepository userRepository,
        IPetsRepository petsRepository,
        TransactionRepository transactionRepository,
        IClanRepository clanRepository,
        IBaseRepository baseRepository
        )

    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPetsRepository _petsRepository = petsRepository;
        private readonly IClanRepository _clanRepository = clanRepository;
        private readonly IBaseRepository _baseRepository = baseRepository;


        private readonly TransactionRepository _transactionRepository = transactionRepository;

        public async System.Threading.Tasks.Task CreateNewClan(string clanName, long userId)
        {
            using var transaction = _transactionRepository.BeginTransaction();
            try
            {
                Pet pet = await _petsRepository.GetPetByUserId(userId);

                Clan clan = new()
                {
                    Name = clanName,
                    WonBattlesCount = 0,
                    AverageClanPower = pet.AveragePower,
                };
                await _userRepository.CreateNewClan(clan);
                await _baseRepository.SaveChangesAsync();
                UserClan userClan = new()
                {
                    ClanId = clan.Id,
                    UserId = userId,
                    RoleId = 1
                };
                await _userRepository.AddUserClan(userClan);
                await _baseRepository.SaveChangesAsync();
                transaction.Commit();

            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }

        public async System.Threading.Tasks.Task DeleteClan(long userId)
        {
            Clan clan = await _userRepository.GetClanByUser(userId);
            
            if (clan.CreatorId != userId)
            {
                throw new Exception("User does not have rights to do this action");
            }

            _baseRepository.Remove(clan);
            await _baseRepository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task AddUserToClan(long userId, AddUserToClanRequest request)
        {
            Clan clan = await _clanRepository.GetClanById(request.ClanId);
            
            if (clan.CreatorId != userId)
            {
                throw new Exception("User does not have rights to do this action");
            }
            
            UserClan userClan = new()
            {
                UserId = request.UserIdToAdd,
                ClanId = clan.Id,
                RoleId = request.RoleId
            };

            await _baseRepository.AddAsync(userClan);
            await _baseRepository.SaveChangesAsync();
        }

        public async Task<PagedList<ClanDTO>> GetAllClans(QueryParameters parameters)
        {
            try
            {
                IQueryable<Clan> clans = _clanRepository.GetAllClans();

                clans = clans.Sort(parameters.SortBy, parameters.SortDescending);

                clans = clans.Filter(parameters.FilterBy, parameters.FilterValue);

                int totalCount = await clans.CountAsync();

                clans = clans.Paginate(parameters.Page, parameters.PageSize);

                List<ClanDTO> clansDto = ClanMapper.ToDTO(clans.ToList());

                return new PagedList<ClanDTO>(clansDto, parameters.Page, parameters.PageSize, totalCount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PagedList<UserRatingTableDTO>> GetClanUserRating(long clanId, QueryParameters parameters)
        {
            try
            {
                IQueryable<User> users = _userRepository.GetUsersByClanId(clanId);

                int totalCount = await users.CountAsync();

                users = users.Sort(parameters.SortBy, parameters.SortDescending);

                users = users.Filter(parameters.FilterBy, parameters.FilterValue);

                users = users.Paginate(parameters.Page, parameters.PageSize);

                List<UserRatingTableDTO> clansDto = users
                    .Select(u => UserMapper.ToRatingTableDTO(u))
                    .ToList();

                return new PagedList<UserRatingTableDTO>(clansDto, parameters.Page, parameters.PageSize, totalCount);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
