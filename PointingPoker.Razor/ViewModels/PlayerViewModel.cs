namespace PointingPoker.Razor.ViewModels;

public record PlayerViewModel(Guid Id, string Name, DateTime TimeJoined, int? Points, bool IsObserver);
