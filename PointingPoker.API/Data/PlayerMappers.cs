using PointingPoker.Models;

namespace PointingPoker.API.Data;

public static class PlayerMappers
{
    public static PlayerDto AsDto(this Player player)
    {
        if (player is null)
        {
            throw new ArgumentNullException(nameof(player));
        }

        return new PlayerDto(player.Id, player.Name, player.TimeJoined, player.Point, player.IsObserver);
    }
}
