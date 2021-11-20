using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Hubs;

public interface IGameHub
{
    Task NotifyNewPlayer(PlayerViewModel player);

    Task NotifyNewVote(PlayerVoteViewModel point);
}
