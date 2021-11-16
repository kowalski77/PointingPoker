namespace PointingPoker.Models;

public record PlayerDto(Guid Id, string Name, DateTime TimeJoined, int? Points, bool IsObserver);
