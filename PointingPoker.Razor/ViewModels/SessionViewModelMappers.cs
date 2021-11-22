using PointingPoker.Models;

namespace PointingPoker.Razor.ViewModels;

public static class SessionViewModelMappers
{
    private static readonly Lazy<PointsViewModelCollection> PointViewModelCollection = new(() => new PointsViewModelCollection());

    private static PointsViewModelCollection PointsViewModelCollection => PointViewModelCollection.Value;

    public static SessionWithPlayersViewModel AsViewModel(this SessionWithPlayersDto source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return new SessionWithPlayersViewModel(
            source.Id,
            source.SessionId,
            source.Players.Select(x => new PlayerViewModel(x.Id, source.SessionId, x.Name, x.TimeJoined, x.Points, x.IsObserver)),
            source.PointsAvailable.Select(GetPointsViewModel));
    }

    private static PointsViewModel GetPointsViewModel(int id)
    {
        var point = PointsViewModelCollection.First(x => x.Value == id);

        return point;
    }
}
