using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Hubs;

public interface IGameConnectionHub
{
    Task NotifyNewPlayer(PlayerViewModel player);

    void OnPlayerReceived(Action<PlayerViewModel> onPlayerReceived);

    Task StartAsync();
}
