using Blazorise;
using Microsoft.AspNetCore.Components;

namespace PointingPoker.Razor.Components;

public class JoinSessionBase : ComponentBase
{
    protected int? SessionNumber { get; set; }

    protected string PlayerName { get; set; } = string.Empty;

    protected Validations Validations { get; set; } = default!;

    protected async Task OnJoinClickedAsync()
    {
        var isValid = this.Validations.ValidateAll();
        if (!isValid)
        {
            return;
        }

    }
}
