using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Hubs;

public interface IGameConnectionHub
{
    Task NotifyNewPlayer(PlayerViewModel player);
    
    Task NotifyNewVote(PlayerVoteViewModel point);

    void OnPlayerReceived(Action<PlayerViewModel> onPlayerReceived);
    
    void OnVoteReceived(Action<PlayerVoteViewModel> onVoteReceived);

    Task StartAsync();
}
