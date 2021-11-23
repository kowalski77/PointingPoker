using System.Diagnostics.CodeAnalysis;

namespace PointingPoker.Razor.ViewModels;

[SuppressMessage("Usage", "CA2225", MessageId = "Operator overloads have named alternates")]
public class ScoreViewModel
{
    private string points = string.Empty;

    public Guid PlayerId { get; init; }

    public string PlayerName { get; private init; } = string.Empty;

    public string Points
    {
        get => this.points;
        set
        {
            this.points = GetValue(value);
        }
    }

    public static explicit operator ScoreViewModel(PlayerViewModel playerViewModel)
    {
        if (playerViewModel == null)
        {
            throw new ArgumentNullException(nameof(playerViewModel));
        }

        return new ScoreViewModel
        {
            PlayerId = playerViewModel.Id,
            PlayerName = playerViewModel.Name,
            Points = playerViewModel.Points.ToString() ?? string.Empty
        };
    }


    // TODO: review this, this logic is in more places in the application
    private static string GetValue(string original)
    {
        var value = original switch
        {
            "999" => "?",
            "9999" => "0",
            _ => original.ToString() ?? string.Empty
        };

        return value;
    }
}
