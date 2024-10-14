using Microsoft.AspNetCore.SignalR;

namespace FreshCode.Hubs
{
    public class BattleHub : Hub
    {

        public async Task SendAction(string battleId, string playerId, string action)
        {
            // Логика отправки действия оппоненту
            await Clients.Group(battleId).SendAsync("ReceiveAction", playerId, action);
        }

        public async Task JoinBattle(string battleId, string playerId)
        {
            // Присоединение к группе битвы
            await Groups.AddToGroupAsync(Context.ConnectionId, battleId);
        }

        public async Task LeaveBattle(string battleId)
        {
            // Покинуть группу битвы
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, battleId);
        }
    }
}
