
using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Repositories;
using FreshCode.Requests;

namespace FreshCode.UseCases
{
    public class ClanUseCase(IUserRepository userRepository, IPetsRepository petsRepository, TransactionRepository transactionRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPetsRepository _petsRepository = petsRepository;
        private readonly TransactionRepository _transactionRepository = transactionRepository;

        public async System.Threading.Tasks.Task CreateNewClan(string clanName, string vk_user_id)
        {
            using var transaction = _transactionRepository.BeginTransaction();
            try
            {
                User user = await _userRepository.GetUserByVkId(vk_user_id);

                Pet pet = await _petsRepository.GetPetByUserId(user.Id);

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
                    UserId = user.Id,
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
    }
}
