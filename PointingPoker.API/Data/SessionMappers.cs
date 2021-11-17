using System.Globalization;
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

    public static GameSession AsEntity(this CreateSessionModel model)
    {
        if (model is null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var session = new GameSession
        {
            Players = new List<Player>(new[]
            {
                new Player { Name = model.playerName, IsObserver = true}
            }),
            PointVotes = model.PointsCollection.Select(x => new Point
            {
                Id = x,
                Name = x.ToString(CultureInfo.InvariantCulture)
            }).ToList()
        };

        return session;
    }
}
