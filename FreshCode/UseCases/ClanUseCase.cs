
using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Repositories;
using FreshCode.Requests;

namespace FreshCode.UseCases
{
    public class ClanUseCase(IUserRepository userRepository, IPetsRepository petsRepository, TransactionRepository transactionRepository, IClanRepository clanRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPetsRepository _petsRepository = petsRepository;
        private readonly IClanRepository _clanRepository = clanRepository;

        private readonly TransactionRepository _transactionRepository = transactionRepository;

        public async System.Threading.Tasks.Task CreateNewClan(string clanName, string vk_user_id)
        {
            using var transaction = _transactionRepository.BeginTransaction();
            try
            {
                long userId = await _userRepository.GetUserIdByVkId(vk_user_id);

                Pet pet = await _petsRepository.GetPetByUserId(userId);

                Clan clan = new()
                {
                    Name = clanName,
                    WonBattlesCount = 0,
                    AverageClanPower = pet.AveragePower,
                };
                await _userRepository.CreateNewClan(clan);
                await _userRepository.SaveChangesAsync();
                UserClan userClan = new()
                {
                    ClanId = clan.Id,
                    UserId = userId,
                    RoleId = 1
                };
                await _userRepository.AddUserClan(userClan);
                await _userRepository.SaveChangesAsync();
                transaction.Commit();

            }
            catch (Exception)
            {
                transaction.Rollback();
            }
        }

        public async System.Threading.Tasks.Task DeleteClan(string vk_user_id)
        {
            long userId = await _userRepository.GetUserIdByVkId(vk_user_id);
            Clan clan = await _userRepository.GetClanByUser(userId);
            
            _clanRepository.DeleteClan(clan);
            await _userRepository.SaveChangesAsync();
        }
    }
}
