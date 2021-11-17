using PointingPoker.Models;

namespace PointingPoker.Razor.ViewModels;

public static class SessionViewModelMappers
{
    public static SessionViewModel AsViewModel(this SessionWithPlayersDto source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return new SessionViewModel(source.Id, source.SessionId, source.Players.Select(x => 
            new PlayerViewModel(x.Id, x.Name, x.TimeJoined, x.Points, x.IsObserver)));
    }
}
