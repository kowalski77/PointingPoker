using Microsoft.AspNetCore.Components;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Components;

public class PlayerBase : ComponentBase
{
    [Parameter] public PlayerViewModel? Model { get; set; }
}
