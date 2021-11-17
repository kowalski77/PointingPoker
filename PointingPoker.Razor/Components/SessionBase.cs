using Blazored.SessionStorage;
using Blazorise;
using Microsoft.AspNetCore.Components;
using PointingPoker.Razor.Services;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Components;

public class SessionBase : ComponentBase
{
    [Inject] private ISessionStorageService SessionStorage { get; set; } = default!;

    [Inject] private INotificationService NotificationService { get; set; } = default!;

    [Inject] private IPokerSessionService PokerSessionService { get; set; } = default!;

    [Parameter] public Guid Id { get; set; }

    private SessionViewModel? session;

    protected override async Task OnParametersSetAsync()
    {
        var result = await this.PokerSessionService.GetSessionWithPlayersAsync(this.Id).ConfigureAwait(false);
        if (result.Failure)
        {
            await this.NotificationService.Error("Ups!!! something went wrong...").ConfigureAwait(false);
        }

        this.session = result.Value;
    }
}
