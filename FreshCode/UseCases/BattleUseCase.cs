using FreshCode.DbModels;
using FreshCode.Hubs;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Requests;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace FreshCode.UseCases
{
    public class BattleUseCase
    {
        private readonly IBattleRepository _battleRepository;
        private readonly IPetsRepository _petRepository;
        private readonly IBaseRepository _baseRepository;
        private readonly IHubContext<BattleHub> _battleHub;

        public BattleUseCase(IBattleRepository battleRepository,
            IPetsRepository petsRepository,
            IBaseRepository baseRepository,
            IHubContext<BattleHub> battleHub)
        {
            _battleRepository = battleRepository;
            _petRepository = petsRepository;
            _baseRepository = baseRepository;
            _battleHub = battleHub;
        }
        public async Task<PetDTO> FindOpponent(long userId)
        {
            Pet pet = await _petRepository.GetPetByUserId(userId);
            
            //проверить, если пользователь находится уже в очереди...
            
            IQueryable<long> opponents = _battleRepository.GetPetOpponents(pet);

            if (opponents.Count() <= 0)
            {
                return null;
            }

            Random random = new Random();

            int index = random.Next(0, opponents.Count());
            var opponentsList = await opponents.ToListAsync();
            var selectedOpponentId = opponentsList[index];

            Pet opponent = await _petRepository.GetPetById(selectedOpponentId);

            return PetMapper.ToDto(opponent);
        }

        public async System.Threading.Tasks.Task JoinBattleQueue(long userId)
        {
            Pet pet = await _petRepository.GetPetByUserId(userId);

            BattleQueue battleQueue = new()
            {
                PetId = pet.Id,
                UserId = userId,
                PetLevel = pet.Level.LevelValue,
                CreatedAt = DateTime.UtcNow,
            };
            await _baseRepository.AddAsync(battleQueue);

            await _baseRepository.SaveChangesAsync();
            var opponent = await FindOpponent(userId);
            if (opponent != null)
            {
                var battle = await CreateBattle(userId, opponent.UserId);

                // Уведомляем пользователей через ConnectionId
                await _battleHub.Clients.Client(BattleHub._userConnections[userId.ToString()])
                    .SendAsync("BattleStarted", opponent.UserId);
                await _battleHub.Clients.Client(BattleHub._userConnections[opponent.UserId.ToString()])
                    .SendAsync("BattleStarted", userId);

                var groupName = battle.Id.ToString();

                // Добавляем их в группу боя
                await _battleHub.Groups.AddToGroupAsync(BattleHub._userConnections[userId.ToString()], groupName);
                await _battleHub.Groups.AddToGroupAsync(BattleHub._userConnections[opponent.UserId.ToString()], groupName);

                await _battleHub.Clients.Client(BattleHub._userConnections[userId.ToString()])
                    .SendAsync("GroupAssigned", groupName);  // Уведомление для userId
                await _battleHub.Clients.Client(BattleHub._userConnections[opponent.UserId.ToString()])
                    .SendAsync("GroupAssigned", groupName);

            }
        }

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
    }
}
