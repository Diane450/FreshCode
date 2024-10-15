using FreshCode.Controllers;
using FreshCode.Services;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel;

namespace FreshCode.Hubs
{
    public class BattleHub : Hub
    {
        private readonly BattleService _battleService;

        public static readonly Dictionary<string, string> _userConnections = new();
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
        }

        // Сообщение для начала боя
        // Метод для обработки атаки
        public async Task Attack(string battleId, string attackerId, string defenderId)
        {
            // Обработка атаки через сервис
            await _battleService.HandleAttack(battleId, attackerId, defenderId);
        }
    }
}
