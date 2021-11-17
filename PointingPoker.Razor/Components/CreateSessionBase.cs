using Blazored.SessionStorage;
using Blazorise;
using Microsoft.AspNetCore.Components;
using PointingPoker.Models;
using PointingPoker.Razor.Services;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Components;

public class CreateSessionBase : ComponentBase
{
    [Inject] private IPokerSessionService PokerSessionService { get; set; } = default!;

    [Inject] private INotificationService NotificationService { get; set; } = default!;

    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Inject] private ISessionStorageService SessionStorage { get; set; } = default!;

    protected Validations Validations { get; set; } = default!;

    protected string PlayerName { get; set; } = string.Empty;

    protected PointsViewModelCollection PointsCollection { get; } = new();

    protected async Task OnCreateSessionClickAsync()
    {
        var isValid = this.Validations.ValidateAll();
        if (!isValid)
        {
            return;
        }

        await this.SessionStorage.SetItemAsync("Player", this.PlayerName).ConfigureAwait(false);

        var selectedPoints = this.PointsCollection.Where(x => x.IsChecked).Select(x => x.Value);
        var sessionModel = new CreateSessionModel(this.PlayerName!, selectedPoints);

        var result = await this.PokerSessionService.CreateAsync(sessionModel).ConfigureAwait(false);
        if (result.Failure)
        {
            await this.NotificationService.Error("Ups!!! something went wrong...").ConfigureAwait(false);
        }

        this.NavigationManager.NavigateTo($"/session/{result.Value}");
    }
}
