using System.Globalization;
using Microsoft.AspNetCore.Components;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Components;

public class CardsBase : ComponentBase
{
    [Parameter]
    [EditorRequired]
    public IEnumerable<PointsViewModel>? Items { get; init; } = new List<PointsViewModel>();

    [Parameter] [EditorRequired] public EventCallback<int> OnVote { get; set; }

    protected string? Vote { get; private set; }

    protected async Task OnFigureClick(int point)
    {
        this.Vote = point switch
        {
            999 => "?",
            9999 => "0",
            _ => point.ToString(CultureInfo.InvariantCulture)
        };

        await this.OnVote.InvokeAsync(point).ConfigureAwait(false);
    }
}
