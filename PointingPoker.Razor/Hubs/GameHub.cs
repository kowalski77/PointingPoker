using Microsoft.AspNetCore.SignalR;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Hubs;

public class GameHub : Hub<IGameClient>, IGameHub
{
    public const string HubUrl = "/game";

    public async Task NotifyNewPlayer(PlayerViewModel player)
    {
        await Clients.All.OnNewPlayer(player).ConfigureAwait(false);
    }
}
