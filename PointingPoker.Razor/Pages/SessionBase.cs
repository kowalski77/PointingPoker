using Blazored.SessionStorage;
using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using PointingPoker.Razor.Hubs;
using PointingPoker.Razor.Services;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Pages;

public class SessionBase : ComponentBase
{
    private string storagePlayer = string.Empty;
    private HubConnection? hubConnection;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private ISessionStorageService SessionStorage { get; set; } = default!;

    [Inject] private INotificationService NotificationService { get; set; } = default!;

    [Inject] private IPokerSessionService PokerSessionService { get; set; } = default!;

    [Parameter] public Guid Id { get; set; }

    protected SessionWithPlayersViewModel? SessionViewModel { get; private set; }

    protected PlayerViewModel? CurrentPlayer { get; private set; }

    protected ICollection<PointsViewModel>? PointsViewModel => this.SessionViewModel?.PointsAvailable.ToList();

    public bool IsModerator => this.CurrentPlayer is not null && this.CurrentPlayer.IsObserver;

    protected bool IsConnected { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var hubUrl = this.NavigationManager.BaseUri.TrimEnd('/') + GameHub.HubUrl;
        this.hubConnection = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .Build();

        this.hubConnection.On<string, string>("Broadcast", this.BroadcastMessage);
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
        this.CurrentPlayer = this.SessionViewModel?.Players.FirstOrDefault(x => x.Name == storagePlayer);
        if (this.CurrentPlayer is not null)
        {
            await this.NotifyAsync().ConfigureAwait(false);
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

    protected async Task NotifyAsync()
    {
        await this.hubConnection!.SendAsync("Broadcast", this.CurrentPlayer?.Name, string.Empty).ConfigureAwait(false);
    }

    private void BroadcastMessage(string name, string message)
    {
        var isMine = name.Equals(this.CurrentPlayer?.Name, StringComparison.OrdinalIgnoreCase);
        if (isMine)
        {
            return;
        }

        // Inform blazor the UI needs updating
        //this.StateHasChanged();
    }
}
