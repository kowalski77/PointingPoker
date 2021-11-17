namespace PointingPoker.Models;

public record AddPlayerModel(int SessionId, string Name);

public record CreateSessionModel(string playerName, IEnumerable<int> PointsCollection);

public record SessionDto(Guid Id, int SessionId);

public record SessionWithPlayersDto(Guid Id, int SessionId, IEnumerable<PlayerDto> Players, IEnumerable<int> PointsAvailable);
