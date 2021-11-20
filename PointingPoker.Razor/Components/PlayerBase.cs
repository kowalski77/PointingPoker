using Microsoft.AspNetCore.Components;
using PointingPoker.Razor.Hubs;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Components;

public class PlayerBase : ComponentBase
{
    [Parameter] public PlayerViewModel? Model { get; set; }

    [Parameter] public IEnumerable<PointsViewModel>? Points { get; set; }
    
    [Inject] private IGameConnectionHub GameConnectionHub { get; set; } = default!;
    
    protected async Task NotifyVote(int vote)
    {
        var playerVote = new PlayerVoteViewModel
        {
            PlayerId = this.Model!.Id,
            Points = vote
        };
        
        await this.GameConnectionHub.NotifyNewVote(playerVote).ConfigureAwait(false);
    }
}
