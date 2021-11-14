using PointingPoker.API.Data;

namespace PointingPoker.API.Models;

public record PlayerDto(Guid Id, string Name, DateTime TimeJoined, int? Points, bool IsObserver)
{
    public static explicit operator PlayerDto(Player player)
    {
        if (player is null)
        {
            throw new ArgumentNullException(nameof(player));
        }

        return new PlayerDto(player.Id, player.Name, player.TimeJoined, player.Points?.Id, player.IsObserver);
    }

    public static PlayerDto ToPlayerDto(PlayerDto left, PlayerDto right)
    {
        throw new NotImplementedException();
    }
}
