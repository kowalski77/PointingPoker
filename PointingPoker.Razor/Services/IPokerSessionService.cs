using PointingPoker.Common.Results;
using PointingPoker.Models;

namespace PointingPoker.Razor.Services;

public interface IPokerSessionService
{
    Task<Result<Guid>> CreateAsync(CreateSessionModel sessionModel);

    Task<Result<SessionWithPlayersDto>> GetSessionWithPlayersAsync(Guid sessionId);
}
