using FreshCode.Services;
using Microsoft.AspNetCore.SignalR;

namespace FreshCode.Hubs
{
    public class BattleHub : Hub
    {
        private readonly BattleService _battleService;

        public BattleHub(BattleService battleService)
        {
            _battleService = battleService;
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).SendAsync("OnConnected", "User has connected to BattleHub");
        }

        // Сообщение для начала боя
        // Метод для обработки атаки
        public async Task Attack(string battleId, string attackerId, string defenderId)
        {
            // Обработка атаки через сервис
            await _battleService.HandleAttack(battleId, attackerId, defenderId);
        }

        // Присоединение к бою
        public async Task JoinBattle(string battleId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, battleId);
        }
    }
}
