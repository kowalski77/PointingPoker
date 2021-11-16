using PointingPoker.Common.Results;

namespace PointingPoker.Razor.Services
{
    public interface ISessionService
    {
        Task<Result<int>> CreateSessionAsync();
    }
}
