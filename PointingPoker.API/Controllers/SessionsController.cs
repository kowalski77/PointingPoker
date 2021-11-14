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
    public async Task<IActionResult> CreateSession()
    {
        var session = new GameSession {SessionId = this.randomNumGenerator.GetRandomNumber(100, 900)};

        this.context.Sessions.Add(session);
        await this.context.SaveChangesAsync().ConfigureAwait(false);

        return this.Ok(session);
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SessionDto>>> GetSessions()
    {
        var sessions = await this.context.Sessions
            .Select(x => new SessionDto(x.Id, x.SessionId, Array.Empty<string>())).ToListAsync()
            .ConfigureAwait(false);

        return this.Ok(sessions);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SessionDto>> GetSession(Guid id)
    {
        var session = await this.context.Sessions
            .Include(x => x.Players)
            .FirstOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);

        if (session is null)
        {
            return this.NotFound();
        }

        return this.Ok(new SessionDto(session.Id, session.SessionId, session.Players.Select(x => x.Name)));
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

        session.Players.Add(new Player {Name = name, TimeJoined = DateTime.UtcNow});
        await this.context.SaveChangesAsync().ConfigureAwait(false);

        return this.Ok();
    }
}
