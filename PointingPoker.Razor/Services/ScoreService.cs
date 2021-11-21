using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Services;

public class ScoreService
{
    //TODO: fix warning
    public event EventHandler<PlayerViewModel> ? PlayerChanged;

    public event EventHandler<PlayerVoteViewModel>? PlayerVoteChanged;
    
    public void AddPlayer(PlayerViewModel player)
    {
        this.PlayerChanged?.Invoke(this, player);
    }

    public void ChangeVote(PlayerVoteViewModel playerVote)
    {
        this.PlayerVoteChanged?.Invoke(this, playerVote);
    }
}
