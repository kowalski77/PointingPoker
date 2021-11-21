using Blazored.SessionStorage;
using Blazorise;
using Microsoft.AspNetCore.Components;
using PointingPoker.Razor.Hubs;
using PointingPoker.Razor.Services;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Pages;

public class SessionBase : ComponentBase
{
    private string storagePlayer = string.Empty;

    [Inject] private ISessionStorageService SessionStorage { get; set; } = default!;

    [Inject] private INotificationService NotificationService { get; set; } = default!;

    [Inject] private IPokerSessionService PokerSessionService { get; set; } = default!;

    [Inject] private IGameConnectionHub GameConnectionHub { get; set; } = default!;
    
    [Inject] private ScoreService ScoreService { get; set; } = default!;

    [Parameter] public Guid Id { get; set; }

    protected SessionWithPlayersViewModel? SessionViewModel { get; private set; }

    protected PlayerViewModel? CurrentPlayer { get; private set; }

    protected IEnumerable<PointsViewModel>? PointsViewModel => this.SessionViewModel?.PointsAvailable.ToList();

    protected bool IsModerator => this.CurrentPlayer is not null && this.CurrentPlayer.IsObserver;

    protected bool IsConnected { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        this.GameConnectionHub.OnPlayerReceived(this.ReceiveNewPlayer);
        this.GameConnectionHub.OnVoteReceived(this.ReceiveVote);
        await this.GameConnectionHub.StartAsync().ConfigureAwait(false);
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
        this.CurrentPlayer = this.SessionViewModel?.Players.FirstOrDefault(x => x.Name == this.storagePlayer);

        if (this.CurrentPlayer is not null)
        {
            await this.GameConnectionHub!.NotifyNewPlayer(this.CurrentPlayer!).ConfigureAwait(false);
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

    private void ReceiveNewPlayer(PlayerViewModel player)
    {
        var isCurrentPlayer = player.Name.Equals(this.CurrentPlayer?.Name, StringComparison.OrdinalIgnoreCase);
        if (isCurrentPlayer)
        {
            return;
        } 
        this.ScoreService.AddPlayer(player);
    }

    private void ReceiveVote(PlayerVoteViewModel pointsViewModel)
    {
        this.ScoreService.ChangeVote(pointsViewModel);
    }
}
