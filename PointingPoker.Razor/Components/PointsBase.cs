using System.Globalization;
using Microsoft.AspNetCore.Components;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Components;

public class PointsBase : ComponentBase
{
    [Parameter] public ICollection<PointsViewModel>? Items { get; init; } = new List<PointsViewModel>();

    protected string? Vote { get; set; }

    protected void OnFigureClick(int point)
    {
        this.Vote = point switch
        {
            999 => "?",
            9999 => "0",
            _ => point.ToString(CultureInfo.InvariantCulture)
        };
    }
}
