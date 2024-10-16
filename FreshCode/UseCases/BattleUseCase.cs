using FreshCode.DbModels;
using FreshCode.Hubs;
using FreshCode.Interfaces;
using FreshCode.Requests;
using FreshCode.Responses;
using Microsoft.AspNetCore.SignalR;

namespace FreshCode.UseCases
{
    public class BattleUseCase
    {
        private readonly IBattleRepository _battleRepository;
        private readonly IPetsRepository _petRepository;
        private readonly IBaseRepository _baseRepository;
        private readonly IHubContext<BattleHub> _battleHub;
        private readonly IUserRepository _userRepository;

        public BattleUseCase(IBattleRepository battleRepository,
            IPetsRepository petsRepository,
            IBaseRepository baseRepository,
            IHubContext<BattleHub> battleHub,
            IUserRepository userRepository)
        {
            _battleRepository = battleRepository;
            _petRepository = petsRepository;
            _baseRepository = baseRepository;
            _battleHub = battleHub;
            _userRepository = userRepository;
        }
        //public async Task<PetDTO> FindOpponent(long userId)
        //{
        //    Pet pet = await _petRepository.GetPetByUserId(userId);

        //    //проверить, если пользователь находится уже в очереди...

        //    IQueryable<long> opponents = _battleRepository.GetPetOpponents(pet);

        //    if (opponents.Count() <= 0)
        //    {
        //        return null;
        //    }

        //    Random random = new Random();

        //    int index = random.Next(0, opponents.Count());
        //    var opponentsList = await opponents.ToListAsync();
        //    var selectedOpponentId = opponentsList[index];

        //    Pet opponent = await _petRepository.GetPetById(selectedOpponentId);

        //    return PetMapper.ToDto(opponent);
        //}

        //public async System.Threading.Tasks.Task JoinBattleQueue(long userId)
        //{
        //    Pet pet = await _petRepository.GetPetByUserId(userId);

        //    BattleQueue battleQueue = new()
        //    {
        //        PetId = pet.Id,
        //        UserId = userId,
        //        PetLevel = pet.Level.LevelValue,
        //        CreatedAt = DateTime.UtcNow,
        //    };
        //    await _baseRepository.AddAsync(battleQueue);

        //    await _baseRepository.SaveChangesAsync();
        //    var opponent = await FindOpponent(userId);
        //    if (opponent != null)
        //    {
        //        var battle = await CreateBattle(userId, opponent.UserId);

        //        var groupName = battle.Id.ToString();

        //        BattleHub._battles.Add(new BattleDTO
        //        {
        //            Attacker = new (BattleHub._userConnections[userId.ToString()], userId),
        //            Defender = new(BattleHub._userConnections[opponent.UserId.ToString()], opponent.UserId),
        //            BattleId = battle.Id,
        //        });

        //        await _battleHub.Groups.AddToGroupAsync(BattleHub._userConnections[userId.ToString()], groupName);
        //        await _battleHub.Groups.AddToGroupAsync(BattleHub._userConnections[opponent.UserId.ToString()], groupName);

        //        await _battleHub.Clients.Client(BattleHub._userConnections[userId.ToString()])
        //            .SendAsync("GroupAssigned", groupName, "Ваш ход");
        //        await _battleHub.Clients.Client(BattleHub._userConnections[opponent.UserId.ToString()])
        //            .SendAsync("GroupAssigned", groupName, "Ход противника");
        //    }
        //}

        public async Task<UserBattle> CreateBattle(long firstPlayerId, long secondPlayerId)
        {
            UserBattle userBattle = new()
            {
                FirstPlayerId = firstPlayerId,
                SecondPlayerId = secondPlayerId,
                CreatedAt = DateTime.UtcNow,
            };
            await _baseRepository.AddAsync(userBattle);
            await _baseRepository.SaveChangesAsync();

            return userBattle;
        }

        public async System.Threading.Tasks.Task Attack(AttackRequest request)
        {
            await _battleHub.Clients.Group(request.BattleId.ToString()).SendAsync("Attack", request.BattleId, request.AttackerId, request.DefenderId);
        }

        public async System.Threading.Tasks.Task UpdateBattle(long battleId, RewardResponse reward, long winnerId, long loserId)
        {
            UserBattle userBattle = await _battleRepository.GetBattleById(battleId);

            userBattle.MoneyReward = reward.Money;
            userBattle.WinnerId = winnerId;
            userBattle.PointsReward = reward.Points;
            userBattle.StatPointsReward = reward.StatPoints;
            userBattle.PrimogemsReward = reward.Primogems;
            userBattle.FinishedAt = DateTime.UtcNow;

            User user = await _userRepository.GetUserById(winnerId);

            user.Money += reward.Money;
            user.StatPoints += reward.StatPoints;
            user.PrimogemsCount = reward.Primogems;
            user.WonBattlesCount += 1;

            Pet winnerPet = await _petRepository.GetPetByUserId(winnerId);

            winnerPet.SleepNeed = winnerPet.SleepNeed - 20 < 0 ? 0 : winnerPet.SleepNeed - 20;
            winnerPet.FeedNeed = winnerPet.FeedNeed - 20 < 0 ? 0 : winnerPet.FeedNeed - 20;

            winnerPet.Points += reward.Points;

            Pet loserPet = await _petRepository.GetPetByUserId(loserId);

            loserPet.SleepNeed = loserPet.SleepNeed - 30 < 0 ? 0 : loserPet.SleepNeed - 30;
            loserPet.FeedNeed = loserPet.FeedNeed - 30 < 0 ? 0 : loserPet.FeedNeed - 30;

            await _baseRepository.SaveChangesAsync();
        }
    }
}
