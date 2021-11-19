using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Hubs;

public interface IGameClient
{
    Task OnNewPlayer(PlayerViewModel player);
}
