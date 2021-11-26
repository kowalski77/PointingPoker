namespace PointingPoker.Razor.ViewModels;

public class PointsViewModel
{
    public bool IsChecked { get; set; }

    public int Value { get; init; }

    public string Display { get; init; } = string.Empty;
}
