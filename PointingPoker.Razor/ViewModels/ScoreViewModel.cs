using System.Diagnostics.CodeAnalysis;

namespace PointingPoker.Razor.ViewModels;

[SuppressMessage("Usage", "CA2225", MessageId = "Operator overloads have named alternates")]
public class ScoreViewModel
{
    public Guid PlayerId { get; init; }

    public string PlayerName { get; private init; } = string.Empty;

    public string Points { get; private set; } = string.Empty;

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
            Points = GetValue(playerViewModel.Points)
        };
    }
    
    public void UpdatePoints(int points)
    {
        Points = GetValue(points);
    }

    // TODO: review this, this logic is in more places in the application
    private static string GetValue(int? original)
    {
        var value = original switch
        {
            999 => "?",
            9999 => "0",
            _ => original.ToString() ?? string.Empty
        };

        return value;
    }
}
