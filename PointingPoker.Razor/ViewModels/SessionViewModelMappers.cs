using PointingPoker.Models;

namespace PointingPoker.Razor.ViewModels;

public static class SessionViewModelMappers
{
    private static readonly Lazy<PointsViewModelCollection> PointViewModelCollection = new(() => new PointsViewModelCollection());

    private static PointsViewModelCollection pointsViewModelCollection => PointViewModelCollection.Value;

    public static SessionWithPlayersViewModel AsViewModel(this SessionWithPlayersDto source)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return new SessionWithPlayersViewModel(
            source.Id,
            source.SessionId,
            source.Players.Select(x => new PlayerViewModel(x.Id, x.Name, x.TimeJoined, x.Points, x.IsObserver)),
            source.PointsAvailable.Select(x => GetPointsViewModel(x)));
    }

    private static PointsViewModel GetPointsViewModel(int id)
    {
        var point = pointsViewModelCollection.First(x => x.Value == id);

        return point;
    }
}
