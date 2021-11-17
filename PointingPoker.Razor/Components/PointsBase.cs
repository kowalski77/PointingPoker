using Microsoft.AspNetCore.Components;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Components;

public class PointsBase : ComponentBase
{
    [Parameter] public List<PointsViewModel>? Items { get; set; }
}
