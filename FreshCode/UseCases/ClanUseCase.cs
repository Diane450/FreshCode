
using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Repositories;
using FreshCode.Requests;

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

            _baseRepository.Remove(clan);
            await _baseRepository.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task AddUserToClan(long userId, long clanId, AddUserToClanRequest request)
        {
            Clan clan = await _clanRepository.GetClanById(clanId);
            
            if (clan.CreatorId != userId)
            {
                throw new Exception("User does not have rights to do this action");
            }
            
            UserClan userClan = new()
            {
                UserId = request.UserIdToAdd,
                ClanId = clanId,
                RoleId = request.RoleId
            };

            await _baseRepository.AddAsync(userClan);
            await _baseRepository.SaveChangesAsync();
        }
    }
}
