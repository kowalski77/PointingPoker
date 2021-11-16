using PointingPoker.Common.Results;
using PointingPoker.Models;

namespace PointingPoker.Razor.Services;

public interface ISessionService
{
    Task<Result<Guid>> CreateSessionAsync(CreateSessionModel sessionModel);
}
