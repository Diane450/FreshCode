using Microsoft.AspNetCore.SignalR;

namespace FreshCode.Hubs
{
    public class BattleHub : Hub
    {
        // Сообщение для начала боя
        public async Task StartBattle(string player1Id, string player2Id)
        {
            await Clients.User(player1Id).SendAsync("BattleStarted", player2Id);
            await Clients.User(player2Id).SendAsync("BattleStarted", player1Id);
        }

        // Сообщение о ходе игрока
        public async Task MakeMove(string battleId, string playerId, string move)
        {
            await Clients.Group(battleId).SendAsync("ReceiveMove", playerId, move);
        }

        // Присоединение к бою
        public async Task JoinBattle(string battleId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, battleId);
        }
    }
}
