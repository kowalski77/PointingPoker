namespace PointingPoker.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class PlayersController : ControllerBase
{
    private readonly PointingPokerContext context;

    public PlayersController(PointingPokerContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PlayerDto>>> GetPlayers()
    {
        var players = await this.context.Players.ToListAsync().ConfigureAwait(false);

        return this.Ok(players.Select(x => (PlayerDto)x).ToList());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PlayerDto>> GetPlayer(Guid id)
    {
        var player = await this.context.Players
            .Include(x => x.Points)
            .FirstOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);
        
        if (player is null)
        {
            return this.NotFound();
        }

        return this.Ok((PlayerDto)player);
    }
    
    [HttpPost("{id:guid}/point")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetPoint(Guid id, [FromBody] int point)
    {
        var player = await this.context.Players
            .Include(x => x.Points)
            .FirstOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);
        
        if (player is null)
        {
            return this.NotFound();
        }

        var pointCard = Point.FindById(point);
        if (pointCard is null)
        {
            return this.BadRequest();
        }
        
        player.Points = pointCard;
        await this.context.SaveChangesAsync().ConfigureAwait(false);

        return this.Ok();
    }
    
    [HttpPost("{id:guid}/reset")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ResetPoints(Guid id)
    {
        var player = await this.context.Players
            .Include(x => x.Points)
            .FirstOrDefaultAsync(x => x.Id == id)
            .ConfigureAwait(false);
        
        if (player is null)
        {
            return this.NotFound();
        }

        player.Points = null;
        await this.context.SaveChangesAsync().ConfigureAwait(false);

        return this.Ok();
    }
}
