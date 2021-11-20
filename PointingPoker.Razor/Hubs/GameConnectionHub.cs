using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Hubs;

public sealed class GameConnectionHub : IGameHub, IAsyncDisposable, IGameConnectionHub
{
    private readonly List<IDisposable> subscriptionCollection = new();
    private readonly HubConnection hubConnection;

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

    public async Task StartAsync()
    {
        await this.hubConnection.StartAsync().ConfigureAwait(false);
    }

    public async Task NotifyNewPlayer(PlayerViewModel player)
    {
        await this.hubConnection.InvokeAsync(nameof(IGameHub.NotifyNewPlayer), player).ConfigureAwait(false);
    }

    public async Task NotifyNewVote(PlayerVoteViewModel point)
    {
        await this.hubConnection.InvokeAsync(nameof(IGameHub.NotifyNewVote), point).ConfigureAwait(false);
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

    public async ValueTask DisposeAsync()
    {
        await this.hubConnection.DisposeAsync().ConfigureAwait(false);
        foreach (var subscription in this.subscriptionCollection)
        {
            subscription.Dispose();
        }
    }
}
