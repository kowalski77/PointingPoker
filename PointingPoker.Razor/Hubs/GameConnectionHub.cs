using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Hubs;

public sealed class GameConnectionHub : IAsyncDisposable, IGameConnectionHub
{
    private readonly HubConnection hubConnection;
    private readonly List<IDisposable> subscriptionCollection = new();

    public GameConnectionHub(NavigationManager navigationManager)
    {
        if (navigationManager is null)
        {
            throw new ArgumentNullException(nameof(navigationManager));
        }

        var hubUrl = $"{navigationManager.BaseUri.TrimEnd('/')}{GameHub.HubUrl}";
        this.hubConnection = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .Build();
    }

    public async ValueTask DisposeAsync()
    {
        await this.hubConnection.DisposeAsync().ConfigureAwait(false);
        foreach (var subscription in this.subscriptionCollection)
        {
            subscription.Dispose();
        }
    }

    public async Task StartAsync()
    {
        if (this.hubConnection.State == HubConnectionState.Disconnected)
        {
            await this.hubConnection.StartAsync().ConfigureAwait(false);
        }
    }

    public async Task NotifyNewPlayer(string sessionGroup, PlayerViewModel player)
    {
        await this.hubConnection.InvokeAsync(nameof(IGameHub.NotifyNewPlayer), sessionGroup, player)
            .ConfigureAwait(false);
    }

    public async Task NotifyNewVote(string sessionGroup, PlayerVoteViewModel point)
    {
        await this.hubConnection.InvokeAsync(nameof(IGameHub.NotifyNewVote), sessionGroup, point).ConfigureAwait(false);
    }

    public async Task NotifyNewUserStory(string sessionGroup, UserStoryViewModel userStory)
    {
        await this.hubConnection.InvokeAsync(nameof(IGameHub.NotifyNewUserStory), sessionGroup, userStory)
            .ConfigureAwait(false);
    }

    public void OnPlayerReceived(Action<PlayerViewModel> onPlayerReceived)
    {
        var subscription = this.hubConnection.On(nameof(IGameClient.OnNewPlayer), onPlayerReceived);
        this.subscriptionCollection.Add(subscription);
    }

    public void OnVoteReceived(Action<PlayerVoteViewModel> onVoteReceived)
    {
        var subscription = this.hubConnection.On(nameof(IGameClient.OnNewVote), onVoteReceived);
        this.subscriptionCollection.Add(subscription);
    }

    public void OnUserStoryReceived(Action<UserStoryViewModel> onUserStoryReceived)
    {
        var subscription = this.hubConnection.On(nameof(IGameClient.OnNewUserStory), onUserStoryReceived);
        this.subscriptionCollection.Add(subscription);
    }
}
