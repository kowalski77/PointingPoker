using System.Globalization;

namespace PointingPoker.Razor.ViewModels;

public class ScoreViewModel
{
    private string points = string.Empty;

    public Guid PlayerId { get; init; }

    public string PlayerName { get; init; } = string.Empty;

    public string Points
    {
        get => this.points;
        set => this.points = GetValue(value);
    }

    // TODO: review this, this logic is in more places in the application
    private static string GetValue(string original)
    {
        var value = original switch
        {
            "999" => "?",
            "9999" => "0",
            _ => original.ToString(CultureInfo.InvariantCulture)
        };

        return value;
    }
}
