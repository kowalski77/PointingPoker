using Microsoft.AspNetCore.Components;

namespace PointingPoker.Razor.Components;

public class SessionBase : ComponentBase
{
    [Parameter] public Guid Id { get; set; }
}
