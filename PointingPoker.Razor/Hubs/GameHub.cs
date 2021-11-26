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

    public async Task NotifyNewVote(PlayerVoteViewModel point)
    {
        await Clients.All.OnNewVote(point).ConfigureAwait(false);
    }

    public async Task NotifyNewUserStory(UserStoryViewModel userStory)
    {
        await Clients.All.OnNewUserStory(userStory).ConfigureAwait(false);
    }
}
