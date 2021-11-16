namespace PointingPoker.Models;

public record SessionDto(Guid Id, int SessionId);

public record SessionWithPlayersDto(Guid Id, int SessionId, IEnumerable<PlayerDto> Players);
