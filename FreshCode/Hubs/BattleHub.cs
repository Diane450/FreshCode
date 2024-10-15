using FreshCode.Controllers;
using FreshCode.ModelsDTO;
using FreshCode.Services;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.ComponentModel;

namespace FreshCode.Hubs
{
    public class BattleHub : Hub
    {
        private readonly BattleService _battleService;

        public static readonly Dictionary<string, string> _userConnections = new();

        private static Dictionary<long, (string ConnectionId, CancellationTokenSource CancelToken)> _waitingPlayers = new();
        
        public static readonly List<BattleDTO> _battles = new();

        public BattleHub(BattleService battleService)
        {
            _battleService = battleService;
        }

        public override async Task OnConnectedAsync()
        {
            //var userId = Context.GetHttpContext()!.Items["userId"];
            var userId = Context.GetHttpContext().Request.Query["userId"];
            _userConnections[userId!.ToString()] = Context.ConnectionId;
            await Clients.Client(Context.ConnectionId).SendAsync("OnConnected", "User has connected to BattleHub");
            await JoinQueue(Context.ConnectionId);
        }

        public async Task JoinQueue(string connectionId)
        {
            //var userId = Context.GetHttpContext()!.Items["userId"];
            var userId = Context.GetHttpContext().Request.Query["userId"];
            await StartLookingForOpponent(Convert.ToInt64(userId), connectionId);
        }

        public async Task StartLookingForOpponent(long userId, string connectionId)
        {
            // Создаем токен для возможности отмены
            var cancellationTokenSource = new CancellationTokenSource();
            _waitingPlayers.Add(userId, (connectionId, cancellationTokenSource));

            // Запускаем таймер отмены поиска через 30 секунд
            var task = Task.Delay(TimeSpan.FromMinutes(1), cancellationTokenSource.Token);

            try
            {
                // Ожидаем завершения задачи поиска соперника
                await Task.WhenAny(task, Matchmaking(userId, cancellationTokenSource.Token));

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
        private async Task Matchmaking(long userId, CancellationToken token)
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
                await Task.Delay(TimeSpan.FromSeconds(1), token);
            }
        }

        // Метод для поиска соперника
        private long? FindOpponent(long userId)
        {
            // Поиск соперника среди игроков в очереди
            foreach (var player in _waitingPlayers)
            {

                // Исключаем текущего игрока из поиска
                if (player.Key != userId)
                {
                    // Если найден соперник, возвращаем его ID
                    return player.Key;
                }
            }

            // Если соперник не найден
            return null;
        }

        // Метод уведомления о найденном сопернике
        private async Task NotifyOpponentFound(long userId, long opponentId)
        {
            // Получаем ConnectionId соперника
            var opponentConnectionId = _waitingPlayers[opponentId].ConnectionId;

            // Уведомляем обоих игроков о найденном сопернике
            await Clients.Client(opponentConnectionId).SendAsync("OpponentFound", userId);
            await Clients.Client(_waitingPlayers[userId].ConnectionId).SendAsync("OpponentFound", opponentId);

        }

        // Метод отмены поиска
        private async Task CancelSearch(long userId)
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
        public async Task Attack(string battleId)
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
