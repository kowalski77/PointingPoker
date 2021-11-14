namespace PointingPoker.API.Models;

public record SessionDto(Guid Id, int SessionId, IEnumerable<string> Players);
