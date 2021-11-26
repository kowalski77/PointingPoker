using Microsoft.AspNetCore.SignalR;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Hubs;

public class GameHub : Hub<IGameClient>, IGameHub
{
    public const string HubUrl = "/game";

    public async Task NotifyNewPlayer(string sessionGroup, PlayerViewModel player)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, sessionGroup).ConfigureAwait(false);
        await this.Clients.Group(sessionGroup).OnNewPlayer(player).ConfigureAwait(false);
    }

    public async Task NotifyNewVote(string sessionGroup, PlayerVoteViewModel point)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, sessionGroup).ConfigureAwait(false);
        await this.Clients.Group(sessionGroup).OnNewVote(point).ConfigureAwait(false);
    }

    public async Task NotifyNewUserStory(string sessionGroup, UserStoryViewModel userStory)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, sessionGroup).ConfigureAwait(false);
        await this.Clients.Group(sessionGroup).OnNewUserStory(userStory).ConfigureAwait(false);
    }
}
