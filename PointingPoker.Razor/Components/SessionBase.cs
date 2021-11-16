using Microsoft.AspNetCore.Components;

namespace PointingPoker.Razor.Components;

public class SessionBase : ComponentBase
{
    [Parameter] public int Id { get; set; }
}
