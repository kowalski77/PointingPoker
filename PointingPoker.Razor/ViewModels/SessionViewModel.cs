namespace PointingPoker.Razor.ViewModels;

public record SessionViewModel(Guid Id, int SessionId, IEnumerable<PlayerViewModel> Players, IEnumerable<PointsViewModel> PointsAvailale);

public record PlayerViewModel(Guid Id, string Name, DateTime TimeJoined, int? Points, bool IsObserver);
