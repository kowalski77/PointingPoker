using Microsoft.AspNetCore.SignalR;

namespace PointingPoker.Razor.Hubs;

public class GameHub : Hub
{
    public const string HubUrl = "/game";

    public async Task Broadcast(string username, string message)
    {
        await Clients.All.SendAsync("Broadcast", username, message).ConfigureAwait(false);
    }

    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"{Context.ConnectionId} connected");
        return base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine($"Disconnected {exception?.Message} {Context.ConnectionId}");
        await base.OnDisconnectedAsync(exception).ConfigureAwait(false);
    }
}
