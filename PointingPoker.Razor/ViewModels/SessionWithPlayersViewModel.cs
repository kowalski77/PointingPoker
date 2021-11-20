namespace PointingPoker.Razor.ViewModels;

public record SessionWithPlayersViewModel(Guid Id, int SessionId, IEnumerable<PlayerViewModel> Players, IEnumerable<PointsViewModel> PointsAvailable);
