using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Hubs;

public interface IGameConnectionHub
{
    Task NotifyNewPlayer(string sessionGroup, PlayerViewModel player);
    
    Task NotifyNewVote(string sessionGroup, PlayerVoteViewModel point);

    Task NotifyNewUserStory(string sessionGroup, UserStoryViewModel userStory);

    void OnPlayerReceived(Action<PlayerViewModel> onPlayerReceived);
    
    void OnVoteReceived(Action<PlayerVoteViewModel> onVoteReceived);

    void OnUserStoryReceived(Action<UserStoryViewModel> onUserStoryReceived);

    Task StartAsync();
}
