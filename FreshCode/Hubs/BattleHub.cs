using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.Mappers;
using FreshCode.ModelsDTO;
using FreshCode.Repositories;
using FreshCode.Services;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel;

namespace FreshCode.Hubs
{
    public class BattleHub : Hub
    {
        private readonly BattleService _battleService;

        public static readonly Dictionary<string, string> _userConnections = new();

        private static Dictionary<long, (string ConnectionId, long InnerId, CancellationTokenSource CancelToken, PetDTO Pet)> _waitingPlayers = new();

        public static readonly List<BattleDTO> _battles = new();

        private readonly IPetsRepository _petRepository;
        private readonly IBaseRepository _baseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IServiceProvider _serviceProvider;

        public BattleHub(BattleService battleService,
            IPetsRepository petRepository,
            IBaseRepository baseRepository,
            IUserRepository userRepository,
            IServiceProvider serviceProvider)
        {
            _battleService = battleService;
            _petRepository = petRepository;
            _baseRepository = baseRepository;
            _userRepository = userRepository;
            _serviceProvider = serviceProvider;
        }

        public override async System.Threading.Tasks.Task OnConnectedAsync()
        {
            //var vk_user_id = Context.GetHttpContext()!.Items["vk_user_id"];
            var vk_user_id = Context.GetHttpContext().Request.Query["vk_user_id"];
            _userConnections[vk_user_id!.ToString()] = Context.ConnectionId;
            await Clients.Client(Context.ConnectionId).SendAsync("OnConnected", "User has connected to BattleHub");
            await JoinQueue(Context.ConnectionId, Convert.ToInt64(vk_user_id));
        }

        public async System.Threading.Tasks.Task JoinQueue(string connectionId, long vk_user_id)
        {
            //var userId = Context.GetHttpContext()!.Items["vk_user_id"];

            //var vk_user_id = Context.GetHttpContext()!.Items["vk_user_id"];

            await StartLookingForOpponent(vk_user_id, connectionId);
        }

        public async System.Threading.Tasks.Task StartLookingForOpponent(long vk_user_id, string connectionId)
        {
            // Создаем токен для возможности отмены
            var cancellationTokenSource = new CancellationTokenSource();

            //var userId = Context.GetHttpContext()!.Items["userId"];

            //var userId = Context.GetHttpContext().Request.Query["userId"];

            var userId = await _userRepository.GetUserIdByVkId(vk_user_id);

            Pet pet = await _petRepository.GetPetByUserId(Convert.ToInt64(userId));

            PetDTO petDTO = PetMapper.ToDto(pet);

            _waitingPlayers.Add(vk_user_id, (connectionId, Convert.ToInt64(userId), cancellationTokenSource, petDTO));

            // Запускаем таймер отмены поиска через 30 секунд
            var task = System.Threading.Tasks.Task.Delay(TimeSpan.FromMinutes(5), cancellationTokenSource.Token);

            try
            {
                // Ожидаем завершения задачи поиска соперника
                await System.Threading.Tasks.Task.WhenAny(task, Matchmaking(vk_user_id, cancellationTokenSource.Token));

                if (task.IsCompleted && !cancellationTokenSource.IsCancellationRequested)
                {
                    // Время вышло, отменяем поиск
                    await CancelSearch(vk_user_id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске соперника: {ex.Message}");
            }
        }

        // Метод поиска соперника (Matchmaking)
        private async System.Threading.Tasks.Task Matchmaking(long vk_user_id, CancellationToken token)
        {
            // Логика поиска соперника
            while (!token.IsCancellationRequested)
            {
                // Ищем соперника в очереди...
                var vk_opponent_id = FindOpponent(vk_user_id);

                if (vk_opponent_id != null)
                {
                    // Если найден соперник
                    await NotifyOpponentFound(vk_user_id, (long)vk_opponent_id);

                    //Удаляем из списка ожидания
                    _waitingPlayers.Remove(vk_user_id);
                    _waitingPlayers.Remove((long)vk_opponent_id);

                    return;
                }

                // Ждем некоторое время перед повторной попыткой
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1), token);
            }
        }

        // Метод для поиска соперника
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

        // Метод уведомления о найденном сопернике
        private async System.Threading.Tasks.Task NotifyOpponentFound(long vk_user_id, long vk_opponent_Id)
        {
            // Получаем ConnectionId соперника
            var opponentConnectionId = _waitingPlayers[vk_opponent_Id].ConnectionId;

            // Уведомляем обоих игроков о найденном сопернике
            await Clients.Client(opponentConnectionId).SendAsync("OpponentFound", vk_user_id, _waitingPlayers[vk_user_id].Pet);
            await Clients.Client(_waitingPlayers[vk_user_id].ConnectionId).SendAsync("OpponentFound", vk_opponent_Id, _waitingPlayers[vk_opponent_Id].Pet);

            var battle = await CreateBattle(_waitingPlayers[vk_user_id].InnerId, _waitingPlayers[vk_opponent_Id].InnerId);

            var groupName = battle.Id.ToString();

            _battles.Add(new BattleDTO
            {
                Attacker = new(_waitingPlayers[vk_user_id].ConnectionId, _waitingPlayers[vk_user_id].InnerId, _waitingPlayers[vk_user_id].Pet, 10),
                Defender = new(_waitingPlayers[vk_opponent_Id].ConnectionId, _waitingPlayers[vk_opponent_Id].InnerId, _waitingPlayers[vk_opponent_Id].Pet, 10),
                BattleId = battle.Id,
            });

            try
            {
                await Groups.AddToGroupAsync(_waitingPlayers[vk_user_id].ConnectionId, groupName);
                await Groups.AddToGroupAsync(_waitingPlayers[vk_opponent_Id].ConnectionId, groupName);

                await Clients.Client(_waitingPlayers[vk_user_id].ConnectionId)
                    .SendAsync("GroupAssigned", groupName, "Ваш ход");
                await Clients.Client(_waitingPlayers[vk_opponent_Id].ConnectionId)
                    .SendAsync("GroupAssigned", groupName, "Ход противника");

                // Отменяем таймер
                _waitingPlayers[vk_user_id].CancelToken.Cancel();
                _waitingPlayers[vk_opponent_Id].CancelToken.Cancel();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                };

                await dbContext.UserBattles.AddAsync(userBattle);
                await dbContext.SaveChangesAsync();

                return userBattle;
            }
        }

        // Метод отмены поиска
        private async System.Threading.Tasks.Task CancelSearch(long userId)
        {
            if (_waitingPlayers.TryGetValue(userId, out var playerData))
            {
                // Уведомляем пользователя об отмене поиска
                await Clients.Client(playerData.ConnectionId)
                    .SendAsync("SearchCanceled", "К сожалению, соперник не был найден. Попробуйте снова через некоторое время.");

                // Удаляем пользователя из очереди
                _waitingPlayers.Remove(userId);

                // Закрываем соединение
                var connection = Clients.Client(playerData.ConnectionId);
                if (connection != null)
                {
                    await connection.SendAsync("OnDisconnected", "Отключение из-за неудачного поиска.");
                    Context.Abort();  // Отключаем клиента от хаба
                }
            }
        }

        public override async System.Threading.Tasks.Task OnDisconnectedAsync(Exception? exception)
        {
            //var vk_user_id = Context.GetHttpContext()!.Items["vk_user_id"];

            var vk_user_id = Context.GetHttpContext().Request.Query["vk_user_id"];

            // Удаляем соединение пользователя из словаря
            if (_userConnections.TryGetValue(vk_user_id!.ToString(), out var connectionId))
            {
                _userConnections.Remove(vk_user_id.ToString());
                await Clients.All.SendAsync("UserDisconnected", $"{vk_user_id} отключился от BattleHub");

                // Удаляем пользователя из очереди
                //var playerInQueue = _waitingPlayers.FirstOrDefault(q => q.Key == Convert.ToInt64(vk_user_id));
                //var playerInQueue = _waitingPlayers[Convert.ToInt64(vk_user_id)];
                if (_waitingPlayers.TryGetValue(Convert.ToInt64(vk_user_id), out var playerInQueue))
                {
                    _waitingPlayers.Remove(Convert.ToInt64(vk_user_id));
                }

                // Удаляем пользователя из боя, если он в нем участвует
                var battleToRemove = _battles.FirstOrDefault(b => b.Attacker.UserId == Convert.ToInt64(vk_user_id) || b.Defender.UserId == Convert.ToInt64(vk_user_id));
                if (battleToRemove != null)
                {
                    _battles.Remove(battleToRemove);
                }
            }
            await base.OnDisconnectedAsync(exception);
        }

        // Сообщение для начала боя
        // Метод для обработки атаки
        public async System.Threading.Tasks.Task Attack(string battleId)
        {
            //var userId = Context.GetHttpContext()!.Items["userId"];
            var userId = Context.GetHttpContext().Request.Query["vk_user_id"].ToString();

            var battle = _battles.Where(b => b.BattleId == Convert.ToInt64(battleId)).First();
            if (Convert.ToInt64(userId) == battle.Attacker.UserId)
            {
                // Обработка атаки через сервис
                await _battleService.HandleAttack(battle);
            }
            else
            {
                await Clients.Client(battle.Defender.ConnectionId).SendAsync("InformPlayerTurn", "Дождитесь хода соперника");
            }
        }
    }
}
