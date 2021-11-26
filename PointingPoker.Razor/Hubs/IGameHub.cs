using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Hubs;

public interface IGameHub
{
    Task NotifyNewPlayer(string sessionGroup, PlayerViewModel player);

    Task NotifyNewVote(string sessionGroup, PlayerVoteViewModel point);
    
    Task NotifyNewUserStory(string sessionGroup, UserStoryViewModel userStory);
    
    Task NotifyVoteVisibility(string sessionGroup, bool isVisible);
}
