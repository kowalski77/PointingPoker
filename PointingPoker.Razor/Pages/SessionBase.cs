using System.Globalization;
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

    private bool votesAreVisible;

    [Inject] private ISessionStorageService SessionStorage { get; set; } = default!;

    [Inject] private INotificationService NotificationService { get; set; } = default!;

    [Inject] private IPokerSessionService PokerSessionService { get; set; } = default!;

    [Inject] private IGameConnectionHub GameConnectionHub { get; set; } = default!;

    [Parameter] public Guid Id { get; set; }

    protected SessionWithPlayersViewModel? SessionViewModel { get; private set; }

    protected PlayerViewModel? CurrentPlayer { get; private set; }

    protected IEnumerable<PointsViewModel>? PointsViewModel => this.SessionViewModel?.PointsAvailable.ToList();

    protected bool IsModerator => this.CurrentPlayer is not null && this.CurrentPlayer.IsObserver;

    protected bool IsConnected { get; private set; }

    protected string UserStory { get; set; } = string.Empty;

    private string SessionId => this.SessionViewModel!.SessionId.ToString(CultureInfo.InvariantCulture);

    protected override async Task OnInitializedAsync()
    {
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
            await this.GameConnectionHub
                .NotifyNewPlayer(this.SessionId, this.CurrentPlayer!)
                .ConfigureAwait(false);
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

    protected async Task OnSetUserStoryClickAsync()
    {
        if (this.SessionViewModel is null)
        {
            await this.NotificationService.Error("Ups!!! something went wrong...").ConfigureAwait(false);
            return;
        }

        await this.GameConnectionHub
            .NotifyNewUserStory(this.SessionId, new UserStoryViewModel(this.UserStory))
            .ConfigureAwait(false);
    }
    
    protected async Task OnChangeVotesVisibility()
    {
        await this.GameConnectionHub.NotifyVoteVisibility(this.SessionId, this.votesAreVisible).ConfigureAwait(false);
        this.votesAreVisible = !this.votesAreVisible;
    }
}
