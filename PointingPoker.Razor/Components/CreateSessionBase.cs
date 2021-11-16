using Blazorise;
using Microsoft.AspNetCore.Components;
using PointingPoker.Razor.Services;

namespace PointingPoker.Razor.Components;

public class CreateSessionBase : ComponentBase
{
    [Inject] private ISessionService SessionService { get; set; } = default!;

    [Inject] private INotificationService notificationService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    protected async Task OnCreateSessionClickAsync()
    {
        var response = await this.SessionService.CreateSessionAsync().ConfigureAwait(false);

    }
}
