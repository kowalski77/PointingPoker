using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Hubs;

public interface IGameConnectionHub
{
    Task NotifyNewPlayer(PlayerViewModel player);
    
    Task NotifyNewVote(PlayerVoteViewModel point);

    Task NotifyNewUserStory(UserStoryViewModel userStory);

    void OnPlayerReceived(Action<PlayerViewModel> onPlayerReceived);
    
    void OnVoteReceived(Action<PlayerVoteViewModel> onVoteReceived);

    void OnUserStoryReceived(Action<UserStoryViewModel> onUserStoryReceived);

    Task StartAsync();
}
