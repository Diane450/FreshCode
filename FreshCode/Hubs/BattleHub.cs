using FreshCode.DbModels;
using FreshCode.Interfaces;
using FreshCode.ModelsDTO;
using FreshCode.Services;
using Microsoft.AspNetCore.SignalR;

namespace FreshCode.Hubs
{
    public class BattleHub : Hub
    {
        private readonly BattleService _battleService;

        public static readonly Dictionary<string, string> _userConnections = new();

        private static Dictionary<long, (string ConnectionId, CancellationTokenSource CancelToken, Pet Pet)> _waitingPlayers = new();

        public static readonly List<BattleDTO> _battles = new();

        private readonly IPetsRepository _petRepository;

        public BattleHub(BattleService battleService, IPetsRepository petRepository)
        {
            _battleService = battleService;
            _petRepository = petRepository;
        }

        public override async System.Threading.Tasks.Task OnConnectedAsync()
        {
            //var userId = Context.GetHttpContext()!.Items["userId"];
            var userId = Context.GetHttpContext().Request.Query["userId"];
            _userConnections[userId!.ToString()] = Context.ConnectionId;
            await Clients.Client(Context.ConnectionId).SendAsync("OnConnected", "User has connected to BattleHub");
            await JoinQueue(Context.ConnectionId);
        }

        public async System.Threading.Tasks.Task JoinQueue(string connectionId)
        {
            //var userId = Context.GetHttpContext()!.Items["userId"];
            var userId = Context.GetHttpContext().Request.Query["userId"];
            await StartLookingForOpponent(Convert.ToInt64(userId), connectionId);
        }

        public async System.Threading.Tasks.Task StartLookingForOpponent(long userId, string connectionId)
        {
            // Создаем токен для возможности отмены
            var cancellationTokenSource = new CancellationTokenSource();

            Pet pet = await _petRepository.GetPetByUserId(userId);

            _waitingPlayers.Add(userId, (connectionId, cancellationTokenSource, pet));

            // Запускаем таймер отмены поиска через 30 секунд
            var task = System.Threading.Tasks.Task.Delay(TimeSpan.FromMinutes(1), cancellationTokenSource.Token);

            try
            {
                // Ожидаем завершения задачи поиска соперника
                await System.Threading.Tasks.Task.WhenAny(task, Matchmaking(userId, cancellationTokenSource.Token));

                if (task.IsCompleted && !cancellationTokenSource.IsCancellationRequested)
                {
                    // Время вышло, отменяем поиск
                    await CancelSearch(userId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при поиске соперника: {ex.Message}");
            }
        }

        // Метод поиска соперника (Matchmaking)
        private async System.Threading.Tasks.Task Matchmaking(long userId, CancellationToken token)
        {
            // Логика поиска соперника
            while (!token.IsCancellationRequested)
            {
                // Ищем соперника в очереди...
                var opponentId = FindOpponent(userId);

                if (opponentId != null)
                {
                    // Отменяем таймер
                    _waitingPlayers[userId].CancelToken.Cancel();
                    _waitingPlayers[(long)opponentId].CancelToken.Cancel();

                    // Если найден соперник
                    await NotifyOpponentFound(userId, (long)opponentId);

                    //Удаляем из списка ожидания
                    _waitingPlayers.Remove(userId);
                    _waitingPlayers.Remove((long)opponentId);

                    return;
                }

                // Ждем некоторое время перед повторной попыткой
                await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(1), token);
            }
        }

        // Метод для поиска соперника
        private long? FindOpponent(long userId)
        {
            var currentPlayer = _waitingPlayers[userId];
            var player = _waitingPlayers.FirstOrDefault(
                p => p.Value.Pet.Level.LevelValue >= currentPlayer.Pet.Level.LevelValue - 1
                && p.Value.Pet.Level.LevelValue <= currentPlayer.Pet.Level.LevelValue + 1
                && p.Value.Pet.Id != currentPlayer.Pet.Id);

            return player.Key;
        }

        // Метод уведомления о найденном сопернике
        private async System.Threading.Tasks.Task NotifyOpponentFound(long userId, long opponentId)
        {
            // Получаем ConnectionId соперника
            var opponentConnectionId = _waitingPlayers[opponentId].ConnectionId;

            // Уведомляем обоих игроков о найденном сопернике
            await Clients.Client(opponentConnectionId).SendAsync("OpponentFound", userId);
            await Clients.Client(_waitingPlayers[userId].ConnectionId).SendAsync("OpponentFound", opponentId);

        }

        // Метод отмены поиска
        private async System.Threading.Tasks.Task CancelSearch(long userId)
        {
            if (_waitingPlayers.TryGetValue(userId, out var playerData))
            {
                // Уведомляем пользователя об отмене поиска
                await Clients.Client(playerData.ConnectionId)
                    .SendAsync("SearchCanceled", "Не удалось найти соперника, поиск отменен.");

                // Удаляем пользователя из очереди
                _waitingPlayers.Remove(userId);
            }
        }


        // Сообщение для начала боя
        // Метод для обработки атаки
        public async System.Threading.Tasks.Task Attack(string battleId)
        {
            //var userId = Context.GetHttpContext()!.Items["userId"];
            var userId = Context.GetHttpContext().Request.Query["userId"].ToString();

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
