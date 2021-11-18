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
    private HubConnection? hubConnection;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private ISessionStorageService SessionStorage { get; set; } = default!;

    [Inject] private INotificationService NotificationService { get; set; } = default!;

    [Inject] private IPokerSessionService PokerSessionService { get; set; } = default!;

    [Parameter] public Guid Id { get; set; }

    protected SessionWithPlayersViewModel? SessionViewModel { get; private set; }

    protected PlayerViewModel? CurrentPlayer { get; private set; }

    protected ICollection<PointsViewModel>? PointsViewModel => this.SessionViewModel?.PointsAvailable.ToList();

    protected override async Task OnParametersSetAsync()
    {
        var result = await this.PokerSessionService.GetSessionWithPlayersAsync(this.Id).ConfigureAwait(false);
        if (result.Failure)
        {
            await this.NotificationService.Error("Ups!!! something went wrong...").ConfigureAwait(false);
        }

        this.SessionViewModel = result.Value;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (!firstRender)
        {
            var storagePlayer = await this.SessionStorage.GetItemAsync<string>("Player").ConfigureAwait(true);
            this.CurrentPlayer = this.SessionViewModel?.Players.First(x => x.Name == storagePlayer);
            this.StateHasChanged();
        }
    }

    protected override async Task OnInitializedAsync()
    {
        var hubUrl = this.NavigationManager.BaseUri.TrimEnd('/') + GameHub.HubUrl;
        this.hubConnection = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .Build();

        this.hubConnection.On<string, string>("Broadcast", this.BroadcastMessage);
        await this.hubConnection.StartAsync().ConfigureAwait(false);
    }

    protected async Task NotifyAsync()
    {
        await this.hubConnection!.SendAsync("Broadcast", this.CurrentPlayer?.Name, "Hello you").ConfigureAwait(false);
    }

    private void BroadcastMessage(string name, string message)
    {
        var isMine = name.Equals(this.CurrentPlayer?.Name, StringComparison.OrdinalIgnoreCase);
        if (isMine)
        {
            return;
        }

        // Inform blazor the UI needs updating
        this.StateHasChanged();
    }
}
