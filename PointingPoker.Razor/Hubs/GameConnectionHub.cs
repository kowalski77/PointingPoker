using Microsoft.AspNetCore.SignalR.Client;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Hubs;

public sealed class GameConnectionHub : IGameHub, IAsyncDisposable
{
    private IDisposable? subscription;
    private readonly HubConnection hubConnection;

    public GameConnectionHub(Uri hubUrl)
    {
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

    public void OnPlayerReceived(Action<PlayerViewModel> onPlayerReceived)
    {
        this.subscription =  this.hubConnection.On(nameof(IGameClient.OnNewPlayer), onPlayerReceived);
    }

    public async ValueTask DisposeAsync()
    {
        await this.hubConnection.DisposeAsync().ConfigureAwait(false);
        this.subscription?.Dispose();   
        this.subscription = null;
    }
}
