using PointingPoker.Common.Results;
using PointingPoker.Models;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Services;

public interface IPokerSessionService
{
    Task<Result<Guid>> CreateAsync(CreateSessionModel model);

    Task<Result<Guid>> AddPlayerToSessionAsync(AddPlayerModel model);

    Task<Result<SessionWithPlayersViewModel>> GetSessionWithPlayersAsync(Guid id);
}
