using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Repositories;
using FreshCode.Services;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Task = System.Threading.Tasks.Task;

namespace FreshCode.Hubs
{
    public class BattleHub : Hub
    {
        private readonly BattleService _battleService;

        private readonly IHubContext<BattleHub> _hubContext;

        public static readonly Dictionary<string, string> _userConnections = new();

        private static Dictionary<long, (string ConnectionId, long InnerId, CancellationTokenSource CancelToken, PetDTO Pet)> _waitingPlayers = new();

        public static readonly List<BattleDTO> _battles = new();

        private readonly IPetsRepository _petRepository;
        private readonly IBaseRepository _baseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IServiceProvider _serviceProvider;

        private readonly static Dictionary<string, CancellationTokenSource> _attackTimers = new();

        public BattleHub(BattleService battleService,
            IPetsRepository petRepository,
            IBaseRepository baseRepository,
            IUserRepository userRepository,
            IServiceProvider serviceProvider,
            IHubContext<BattleHub> hubContext)
        {
            _battleService = battleService;
            _petRepository = petRepository;
            _baseRepository = baseRepository;
            _userRepository = userRepository;
            _serviceProvider = serviceProvider;
            _hubContext = hubContext;
        }

        public override async System.Threading.Tasks.Task OnConnectedAsync()
        {
            var vk_user_id = Context.GetHttpContext().Request.Query["vk_user_id"];
            _userConnections[vk_user_id!.ToString()] = Context.ConnectionId;
            await _hubContext.Clients.Client(Context.ConnectionId).SendAsync("OnConnected", "User has connected to BattleHub");
            await JoinQueue(Context.ConnectionId, Convert.ToInt64(vk_user_id));
        }

        public async System.Threading.Tasks.Task JoinQueue(string connectionId, long vk_user_id)
        {
            await StartLookingForOpponent(vk_user_id, connectionId);
        }

        public async System.Threading.Tasks.Task StartLookingForOpponent(long vk_user_id, string connectionId)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var userId = await _userRepository.GetUserIdByVkId(vk_user_id);
            Pet pet = await _petRepository.GetPetByUserId(Convert.ToInt64(userId));
            PetDTO petDTO = PetMapper.ToDto(pet);

            _waitingPlayers.Add(vk_user_id, (connectionId, Convert.ToInt64(userId), cancellationTokenSource, petDTO));

            var task = System.Threading.Tasks.Task.Delay(TimeSpan.FromMinutes(5), cancellationTokenSource.Token);

            try
            {
                await System.Threading.Tasks.Task.WhenAny(task, Matchmaking(vk_user_id, cancellationTokenSource.Token));
                if (task.IsCompleted && !cancellationTokenSource.IsCancellationRequested)
                {
                    await CancelSearch(vk_user_id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске соперника: {ex.Message}");
            }
        }

        private async System.Threading.Tasks.Task Matchmaking(long vk_user_id, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var vk_opponent_id = FindOpponent(vk_user_id);

                if (vk_opponent_id != null)
                {
                    await NotifyOpponentFound(vk_user_id, (long)vk_opponent_id);

                    _waitingPlayers.Remove(vk_user_id);
                    _waitingPlayers.Remove((long)vk_opponent_id);

                    return;
                }

                await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1), token);
            }
        }

        private long? FindOpponent(long vk_user_id)
        {
            var currentPlayer = _waitingPlayers[vk_user_id];
            var player = _waitingPlayers.FirstOrDefault(
                p => p.Value.Pet.Level >= currentPlayer.Pet.Level - 1
                && p.Value.Pet.Level <= currentPlayer.Pet.Level + 1
                && p.Value.Pet.Id != currentPlayer.Pet.Id);

            if (player.Key != 0)
            {
                return player.Key;
            }
            return null;
        }

        private async System.Threading.Tasks.Task NotifyOpponentFound(long vk_user_id, long vk_opponent_Id)
        {
            var opponentConnectionId = _waitingPlayers[vk_opponent_Id].ConnectionId;

            await _hubContext.Clients.Client(opponentConnectionId).SendAsync("OpponentFound", vk_user_id, _waitingPlayers[vk_user_id].Pet);
            await _hubContext.Clients.Client(_waitingPlayers[vk_user_id].ConnectionId).SendAsync("OpponentFound", vk_opponent_Id, _waitingPlayers[vk_opponent_Id].Pet);

            var battle = await CreateBattle(_waitingPlayers[vk_user_id].InnerId, _waitingPlayers[vk_opponent_Id].InnerId);

            var groupName = battle.Id.ToString();

            _battles.Add(new BattleDTO
            {
                Attacker = new(_waitingPlayers[vk_user_id].ConnectionId, _waitingPlayers[vk_user_id].InnerId, _waitingPlayers[vk_user_id].Pet, vk_user_id, 10),
                Defender = new(_waitingPlayers[vk_opponent_Id].ConnectionId, _waitingPlayers[vk_opponent_Id].InnerId, _waitingPlayers[vk_opponent_Id].Pet, vk_opponent_Id, 10),
                BattleId = battle.Id,
            });

            try
            {
                await _hubContext.Groups.AddToGroupAsync(_waitingPlayers[vk_user_id].ConnectionId, groupName);
                await _hubContext.Groups.AddToGroupAsync(_waitingPlayers[vk_opponent_Id].ConnectionId, groupName);

                await _hubContext.Clients.Client(_waitingPlayers[vk_user_id].ConnectionId)
                    .SendAsync("GroupAssigned", groupName, "Ваш ход");
                await _hubContext.Clients.Client(_waitingPlayers[vk_opponent_Id].ConnectionId)
                    .SendAsync("GroupAssigned", groupName, "Ход противника");

                _waitingPlayers[vk_user_id].CancelToken.Cancel();
                _waitingPlayers[vk_opponent_Id].CancelToken.Cancel();

                StartAttackTimer(battle.Id.ToString(), vk_user_id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void StartAttackTimer(string battleId, long vk_user_id)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            _attackTimers[battleId] = cancellationTokenSource;

            System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(20), cancellationTokenSource.Token).ContinueWith(async task =>
            {
                if (!task.IsCanceled)
                {
                    await ForcePassTurn(battleId, vk_user_id);
                }
            });
        }

        private async System.Threading.Tasks.Task ForcePassTurn(string battleId, long vk_user_id)
        {
            var battle = _battles.FirstOrDefault(b => b.BattleId == Convert.ToInt64(battleId));
            if (battle != null)
            {
                if (battle.Attacker.vk_user_id == vk_user_id)
                {
                    // Получаем кортеж
                    var attacker = battle.Attacker;

                    // Изменяем поле Movecount
                    attacker.Movecount -= 1;

                    // Присваиваем обновленный кортеж обратно
                    battle.Attacker = attacker;

                    await _hubContext.Clients.Client(battle.Attacker.ConnectionId).SendAsync("TurnPassed", "Ваш ход был пропущен", battle.Attacker.Movecount);
                    await _hubContext.Clients.Client(battle.Defender.ConnectionId).SendAsync("OpponentTurn", "Противник пропустил ход. Ваша очередь");
                }
                else
                {
                    await _hubContext.Clients.Client(battle.Defender.ConnectionId).SendAsync("TurnPassed", "Ваш ход был пропущен.");
                    await _hubContext.Clients.Client(battle.Attacker.ConnectionId).SendAsync("OpponentTurn", "Противник пропустил ход. Ваша очередь");
                }

                // Смена хода
                SwapTurns(battleId);
            }
        }

        private void SwapTurns(string battleId)
        {
            var battle = _battles.FirstOrDefault(b => b.BattleId == Convert.ToInt64(battleId));
            if (battle != null)
            {
                var temp = battle.Attacker;
                battle.Attacker = battle.Defender;
                battle.Defender = temp;

                StartAttackTimer(battleId, battle.Attacker.vk_user_id);
            }
        }

        public async System.Threading.Tasks.Task Attack(string battleId)
        {
            var vk_user_Id = Context.GetHttpContext().Request.Query["vk_user_id"].ToString();
            var battle = _battles.Where(b => b.BattleId == Convert.ToInt64(battleId)).First();

            if (Convert.ToInt64(vk_user_Id) == battle.Attacker.vk_user_id)
            {
                if (await _battleService.HandleAttack(battle))
                {
                    await Groups.RemoveFromGroupAsync(battle.Attacker.ConnectionId, battleId);
                    await Groups.RemoveFromGroupAsync(battle.Defender.ConnectionId, battleId);

                    _battles.Remove(battle);
                }
                else
                {
                    _attackTimers[battleId].Cancel();
                    SwapTurns(battleId);
                }
            }
            else
            {
                await _hubContext.Clients.Client(battle.Defender.ConnectionId).SendAsync("InformPlayerTurn", "Дождитесь хода соперника");
            }
        }

        public async Task<UserBattle> CreateBattle(long firstPlayerId, long secondPlayerId)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<FreshCodeContext>();

                UserBattle userBattle = new()
                {
                    FirstPlayerId = firstPlayerId,
                    SecondPlayerId = secondPlayerId,
                    CreatedAt = DateTime.UtcNow,
                    FinishedAt = null
                };

                dbContext.UserBattles.Add(userBattle);
                await dbContext.SaveChangesAsync();

                return userBattle;
            }
        }

        private async Task CancelSearch(long vk_user_id)
        {
            if (_waitingPlayers.ContainsKey(vk_user_id))
            {
                var connectionId = _waitingPlayers[vk_user_id].ConnectionId;
                _waitingPlayers.Remove(vk_user_id);
                await Clients.Client(connectionId).SendAsync("SearchCancelled", "Поиск соперника отменен.");
                Context.Abort();
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var vk_user_id = Context.GetHttpContext().Request.Query["vk_user_id"];
            if (_userConnections.ContainsKey(vk_user_id))
            {
                _userConnections.Remove(vk_user_id);
                await CancelSearch(Convert.ToInt64(vk_user_id));
            }
            await base.OnDisconnectedAsync(exception);
        }

        public async Task Disconnect()
        {
            Context.Abort();
        }
    }
}
