﻿using PointingPoker.Models;

namespace PointingPoker.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class SessionsController : ControllerBase
{
    private readonly PointingPokerContext context;
    private readonly IRandomNumGenerator randomNumGenerator;

    public SessionsController(PointingPokerContext context, IRandomNumGenerator randomNumGenerator)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        this.randomNumGenerator = randomNumGenerator ?? throw new ArgumentNullException(nameof(randomNumGenerator));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SessionDto>> CreateSession([FromBody] CreateSessionModel model)
    {
        if (model is null)
        {
            return this.BadRequest("Model is null");
        }

        var session = new GameSession
        {
            SessionId = this.randomNumGenerator.GetRandomNumber(1, 999),
            Players = new List<Player>(new[]
            {
                new Player { Name = model.playerName, IsObserver = model.IsObserver}
            })
        };

        this.context.Sessions.Add(session);
        await this.context.SaveChangesAsync().ConfigureAwait(false);

        return this.Ok(session.AsDto());
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SessionDto>>> GetSessions()
    {
        var sessions = await this.context.Sessions
            .Select(x => new SessionDto(x.Id, x.SessionId)).ToListAsync()
            .ConfigureAwait(false);

        return this.Ok(sessions);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SessionWithPlayersDto>> GetSession(Guid id)
    {
        var session = await this.context.Sessions
            .Include(x => x.Players)
            .ThenInclude(x => x.Points)
            .FirstOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);

        if (session is null)
        {
            return this.NotFound($"No session found with id: {id}");
        }

        var sessionDto = new SessionWithPlayersDto(
            session.Id,
            session.SessionId,
            session.Players.Select(x => x.AsDto()));

        return this.Ok(sessionDto);
    }

    [HttpPost("{id:guid}/addplayer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddPlayerToSession(Guid id, [FromBody] string name)
    {
        var session = await this.context.Sessions
            .Include(x => x.Players)
            .FirstOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);

        if (session is null)
        {
            return this.NotFound();
        }

        if (session.Players.Any(p => p.Name == name))
        {
            return this.BadRequest("Player with this name already exists");
        }

        session.Players.Add(new Player { Name = name, TimeJoined = DateTime.UtcNow });
        await this.context.SaveChangesAsync().ConfigureAwait(false);

        return this.Ok();
    }
}
