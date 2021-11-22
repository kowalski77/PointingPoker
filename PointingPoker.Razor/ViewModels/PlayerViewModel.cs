namespace PointingPoker.Razor.ViewModels;

public record PlayerViewModel(Guid Id, int SessionId, string Name, DateTime TimeJoined, int? Points, bool IsObserver);
