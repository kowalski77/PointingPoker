using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Hubs;

public interface IGameConnectionHub : IGameHub
{
    void OnPlayerReceived(Action<PlayerViewModel> onPlayerReceived);
    
    void OnVoteReceived(Action<PlayerVoteViewModel> onVoteReceived);

    void OnUserStoryReceived(Action<UserStoryViewModel> onUserStoryReceived);
    
    void OnVoteVisibilityChanged(Action<bool> onVoteVisibilityChanged);

    Task StartAsync();
}
