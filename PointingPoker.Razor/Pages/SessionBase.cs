using Blazored.SessionStorage;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using PointingPoker.Razor.Hubs;
using PointingPoker.Razor.Services;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Pages;

public class SessionBase : ComponentBase, IAsyncDisposable
{
    private List<PlayerViewModel> activePlayers = new();
    private HubConnection? hubConnection;
    private string storagePlayer = string.Empty;
    private IDisposable? subscription;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private ISessionStorageService SessionStorage { get; set; } = default!;

    [Inject] private INotificationService NotificationService { get; set; } = default!;

    [Inject] private IPokerSessionService PokerSessionService { get; set; } = default!;

    [Parameter] public Guid Id { get; set; }

    protected SessionWithPlayersViewModel? SessionViewModel { get; private set; }

    protected PlayerViewModel? CurrentPlayer { get; private set; }

    protected ICollection<PointsViewModel>? PointsViewModel => this.SessionViewModel?.PointsAvailable.ToList();

    protected IEnumerable<PlayerViewModel> ActivePlayers => this.activePlayers;

    protected bool IsModerator => this.CurrentPlayer is not null && this.CurrentPlayer.IsObserver;

    protected bool IsConnected { get; private set; }

    public async ValueTask DisposeAsync()
    {
        if (this.hubConnection is not null)
        {
            await this.hubConnection.DisposeAsync().ConfigureAwait(false);
        }

        this.subscription?.Dispose();
        GC.SuppressFinalize(this);
    }

    protected override async Task OnInitializedAsync()
    {
        var hubUrl = this.NavigationManager.BaseUri.TrimEnd('/') + GameHub.HubUrl;
        this.hubConnection = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .Build();

        this.subscription = this.hubConnection.On<string, string>("Broadcast", this.ReceiveNewPlayer);
        await this.hubConnection.StartAsync().ConfigureAwait(false);
    }

    protected override async Task OnParametersSetAsync()
    {
        var result = await this.PokerSessionService.GetSessionWithPlayersAsync(this.Id).ConfigureAwait(true);
        if (result.Failure)
        {
            await this.NotificationService.Error("Ups!!! something went wrong...").ConfigureAwait(false);
            return;
        }

        this.SessionViewModel = result.Value;
        this.activePlayers = this.SessionViewModel?.Players.ToList() ?? new List<PlayerViewModel>();
        this.CurrentPlayer = this.SessionViewModel?.Players.FirstOrDefault(x => x.Name == this.storagePlayer);

        if (this.CurrentPlayer is not null)
        {
            await this.NotifyNewPlayerAsync().ConfigureAwait(false);
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            this.storagePlayer = await this.SessionStorage.GetItemAsync<string>("Player").ConfigureAwait(false);
            this.IsConnected = true;
        }
    }

    private async Task NotifyNewPlayerAsync()
    {
        await this.hubConnection!.SendAsync("Broadcast", this.CurrentPlayer?.Name, string.Empty).ConfigureAwait(false);
    }

    private void ReceiveNewPlayer(string name, string message)
    {
        var isMine = name.Equals(this.CurrentPlayer?.Name, StringComparison.OrdinalIgnoreCase);
        if (isMine)
        {
            return;
        }

        this.activePlayers.Add(new PlayerViewModel(Guid.NewGuid(), name, DateTime.UtcNow, null, false));
        this.StateHasChanged();
    }
}
