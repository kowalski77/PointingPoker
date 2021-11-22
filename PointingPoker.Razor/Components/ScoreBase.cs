using System.Globalization;
using Microsoft.AspNetCore.Components;
using PointingPoker.Razor.Hubs;
using PointingPoker.Razor.Services;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Components;

public class ScoreBase : ComponentBase
{
    protected ICollection<ScoreViewModel> ScoreViewModels { get; } = new List<ScoreViewModel>();

    [Inject] private IGameConnectionHub GameConnectionHub { get; set; } = default!;
    
    
    [Inject] private IScoreCache ScoreCache { get; set; } = default!;   

    protected override async Task OnInitializedAsync()
    {
        this.GameConnectionHub.OnPlayerReceived(this.ReceiveNewPlayer);
        this.GameConnectionHub.OnVoteReceived(this.ReceiveVote);
        await this.GameConnectionHub.StartAsync().ConfigureAwait(false);
    }
    
    private void ReceiveNewPlayer(PlayerViewModel player)
    {
        var sessionId = player.SessionId.ToString(CultureInfo.InvariantCulture);

        var cachedScores = this.ScoreCache.Get(sessionId)?.ToList() ?? new List<ScoreViewModel>();
        if(cachedScores.Any(x=>x.PlayerId == player.Id))
        {
            return;
        }

        cachedScores.Add((ScoreViewModel)player);

        foreach (var cachedScore in cachedScores)
        {
            this.ScoreViewModels.Add(cachedScore);
            this.StateHasChanged();
        }

        this.ScoreCache.Update(sessionId, cachedScores);
    }

    private void ReceiveVote(PlayerVoteViewModel pointsViewModel)
    {
        var score = this.ScoreViewModels.FirstOrDefault(x => x.PlayerId == pointsViewModel.PlayerId);
        if(score is null)
        {
            return;
        }

        score.UpdatePoints(pointsViewModel.Points);
        
        this.StateHasChanged();
    }
}
