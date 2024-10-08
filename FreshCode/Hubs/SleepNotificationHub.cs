using Microsoft.AspNetCore.SignalR;

namespace FreshCode.Hubs
{
    public class SleepNotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier; // Получаем userId из контекста аутентификации
            if (!string.IsNullOrEmpty(userId))
            {
                // Присоединяем клиента к группе по userId
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var userId = Context.UserIdentifier;
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
