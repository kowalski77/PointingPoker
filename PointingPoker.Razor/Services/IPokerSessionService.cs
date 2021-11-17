using PointingPoker.Common.Results;
using PointingPoker.Models;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Services;

public interface IPokerSessionService
{
    Task<Result<Guid>> CreateAsync(CreateSessionModel sessionModel);

    Task<Result<SessionViewModel>> GetSessionWithPlayersAsync(Guid sessionId);
}
