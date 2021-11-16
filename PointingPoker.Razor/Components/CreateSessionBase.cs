using Blazorise;
using Microsoft.AspNetCore.Components;
using PointingPoker.Models;
using PointingPoker.Razor.Services;

namespace PointingPoker.Razor.Components;

public class CreateSessionBase : ComponentBase
{
    [Inject] private ISessionService SessionService { get; set; } = default!;

    [Inject] private INotificationService NotificationService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    protected string? PlayerName { get; set; }

    protected bool IsModerator { get; set; }

    protected int SessionNumber { get; set; }

    protected Validations Validations { get; set; } = default!;

    protected async Task OnCreateSessionClickAsync()
    {
        var isValid = this.Validations.ValidateAll();
        if (!isValid)
        {
            return;
        }

        var sessionModel = new CreateSessionModel(this.PlayerName!, this.IsModerator);
        var response = await this.SessionService.CreateSessionAsync(sessionModel).ConfigureAwait(false);
        if (response.Failure)
        {
            await this.NotificationService.Error("Ups!!! something went wrong...").ConfigureAwait(false);
        }

        this.NavigationManager.NavigateTo($"/session/{response.Value.SessionId}");
    }
}
