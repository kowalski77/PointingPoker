using Microsoft.AspNetCore.Components;
using PointingPoker.Razor.Services;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Components;

public class ScoreBase : ComponentBase, IDisposable
{
    protected ICollection<ScoreViewModel> ScoreViewModels { get; } = new List<ScoreViewModel>();

    [Inject] private ScoreService ScoreService { get; set; } = default!;

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected override void OnInitialized()
    {
        this.ScoreService.ScoreAdded += this.OnScoreAdded;
        this.ScoreService.ScoreUpdated += this.OnScoreUpdated;
    }

    private void OnScoreAdded(object? sender, ScoreEventArgs e)
    {
        var scoreViewModel = new ScoreViewModel
        {
            PlayerId = e.PlayerId, PlayerName = e.PlayerName, Points = e.Points.ToString() ?? string.Empty
        };

        this.ScoreViewModels.Add(scoreViewModel);

        this.StateHasChanged();
    }

    private void OnScoreUpdated(object? sender, ScoreEventArgs e)
    {
        var player = this.ScoreViewModels.FirstOrDefault(x => x.PlayerId == e.PlayerId);
        if (player is null)
        {
            return;
        }

        player.Points = e.Points.ToString() ?? string.Empty;

        this.StateHasChanged();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        this.ScoreService.ScoreAdded -= this.OnScoreAdded;
        this.ScoreService.ScoreUpdated -= this.OnScoreUpdated;
    }
}
