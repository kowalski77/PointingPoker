using PointingPoker.Models;

namespace PointingPoker.API.Data;

public static class SessionMappers
{
    public static SessionDto AsDto(this GameSession gameSession)
    {
        if (gameSession is null)
        {
            throw new ArgumentNullException(nameof(gameSession));
        }

        return new SessionDto(gameSession.Id, gameSession.SessionId);
    }
}
