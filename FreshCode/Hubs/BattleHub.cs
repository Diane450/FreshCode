using FreshCode.Controllers;
using FreshCode.ModelsDTO;
using FreshCode.Services;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel;

namespace FreshCode.Hubs
{
    public class BattleHub : Hub
    {
        private readonly BattleService _battleService;

        public static readonly Dictionary<string, string> _userConnections = new();
        
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
