using Blazorise;
using Microsoft.AspNetCore.Components;

namespace PointingPoker.Razor.Components;

public class JoinSessionBase : ComponentBase
{
    protected int? SessionNumber { get; set; }

    protected string PlayerName { get; set; } = string.Empty;

    protected Validations Validations { get; set; } = default!;
}
