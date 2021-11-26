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

    protected override async Task OnInitializedAsync()
    {
        this.GameConnectionHub.OnUserStoryReceived(this.ReceiveUserStory);
        await this.GameConnectionHub.StartAsync().ConfigureAwait(false);
    }

    protected async Task NotifyVote(int vote)
    {
        var playerVote = new PlayerVoteViewModel {PlayerId = this.Model!.Id, Points = vote};

        await this.GameConnectionHub.NotifyNewVote(playerVote).ConfigureAwait(false);
    }

    private void ReceiveUserStory(UserStoryViewModel userStoryViewModel)
    {
        if(userStoryViewModel.SessionId != this.Model?.SessionId)
        {
            return;
        }
        
        this.UserStory = userStoryViewModel.Text;
        this.StateHasChanged();
    }
}
