using System.Globalization;
using Microsoft.AspNetCore.Components;
using PointingPoker.Razor.Hubs;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Components;

public class PlayerBase : ComponentBase
{
    [Parameter] public IEnumerable<PointsViewModel>? Points { get; set; }

    [Inject] private IGameConnectionHub GameConnectionHub { get; set; } = default!;

    [Parameter] public PlayerViewModel? Model { get; set; }

    protected string UserStory { get; set; } = string.Empty;

    private string SessionId => this.Model!.SessionId.ToString(CultureInfo.InvariantCulture);

    protected override async Task OnInitializedAsync()
    {
        this.GameConnectionHub.OnUserStoryReceived(this.ReceiveUserStory);
        await this.GameConnectionHub.StartAsync().ConfigureAwait(false);
    }

    protected async Task NotifyVote(int vote)
    {
        var playerVote = new PlayerVoteViewModel {PlayerId = this.Model!.Id, Points = vote};

        await this.GameConnectionHub.NotifyNewVote(this.SessionId, playerVote).ConfigureAwait(false);
    }

    private void ReceiveUserStory(UserStoryViewModel userStoryViewModel)
    {
        this.UserStory = userStoryViewModel.Text;
        this.StateHasChanged();
    }
}
