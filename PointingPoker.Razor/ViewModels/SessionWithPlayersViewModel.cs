namespace PointingPoker.Razor.ViewModels;

public record SessionWithPlayersViewModel(Guid Id, int SessionId, IEnumerable<PlayerViewModel> Players, IEnumerable<PointsViewModel> PointsAvailable);

public record PlayerViewModel(Guid Id, string Name, DateTime TimeJoined, int? Points, bool IsObserver);
