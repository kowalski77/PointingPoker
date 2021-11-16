using Blazorise;
using Microsoft.AspNetCore.Components;
using PointingPoker.Razor.Services;

namespace PointingPoker.Razor.Components;

public class CreateSessionBase : ComponentBase
{
    [Inject] private ISessionService SessionService { get; set; } = default!;

    [Inject] private INotificationService NotificationService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    protected async Task OnCreateSessionClickAsync()
    {
        var response = await this.SessionService.CreateSessionAsync().ConfigureAwait(false);
        if (response.Failure)
        {
            await this.NotificationService.Error("Ups!!! something went wrong...").ConfigureAwait(false);
        }

        this.NavigationManager.NavigateTo($"/session/{response.Value.SessionId}");
    }
}
