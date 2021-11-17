using Blazored.SessionStorage;
using Blazorise;
using Microsoft.AspNetCore.Components;
using PointingPoker.Models;
using PointingPoker.Razor.Services;

namespace PointingPoker.Razor.Components;

public class JoinSessionBase : ComponentBase
{
    [Inject] private IPokerSessionService PokerSessionService { get; set; } = default!;

    [Inject] private INotificationService NotificationService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private ISessionStorageService SessionStorage { get; set; } = default!;

    protected int SessionNumber { get; set; }

    protected string PlayerName { get; set; } = string.Empty;

    protected Validations Validations { get; set; } = default!;

    protected async Task OnJoinClickedAsync()
    {
        var isValid = this.Validations.ValidateAll();
        if (!isValid)
        {
            return;
        }

        await this.SessionStorage.SetItemAsync("Player", this.PlayerName).ConfigureAwait(false);
        var result = await this.PokerSessionService.AddPlayerToSessionAsync(new AddPlayerModel(this.SessionNumber, this.PlayerName)).ConfigureAwait(false);
        if (result.Failure)
        {
            await this.NotificationService.Error("Ups!!! something went wrong...").ConfigureAwait(false); // TODO: not working
        }
        else
        {
            this.NavigationManager.NavigateTo($"/session/{result.Value}");
        }
    }
}
