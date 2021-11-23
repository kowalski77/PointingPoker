﻿using System.Globalization;
using Microsoft.AspNetCore.Components;
using PointingPoker.Razor.Hubs;
using PointingPoker.Razor.Services;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Components;

public class ScoreBase : ComponentBase
{
    protected ICollection<ScoreViewModel> ScoreViewModels { get; private set; } = new List<ScoreViewModel>();

    [Inject] private IGameConnectionHub GameConnectionHub { get; set; } = default!;

    [Inject] private IScoreCache ScoreCache { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        this.GameConnectionHub.OnPlayerReceived(this.ReceiveNewPlayer);
        this.GameConnectionHub.OnVoteReceived(this.ReceiveVote);
        await this.GameConnectionHub.StartAsync().ConfigureAwait(false);
    }

    private void ReceiveNewPlayer(PlayerViewModel player)
    {
        var sessionId = player.SessionId.ToString(CultureInfo.InvariantCulture);

        var newlyScore = (ScoreViewModel)player;

        var cachedScoreViewModels = this.ScoreCache.Get(sessionId)?.ToList() ?? new List<ScoreViewModel>();
        cachedScoreViewModels.Add(newlyScore);
        this.ScoreCache.Update(sessionId, cachedScoreViewModels);

        this.ScoreViewModels = new List<ScoreViewModel>(cachedScoreViewModels);
        this.StateHasChanged();
    }

    private void ReceiveVote(PlayerVoteViewModel pointsViewModel)
    {
        var score = this.ScoreViewModels.FirstOrDefault(x => x.PlayerId == pointsViewModel.PlayerId);
        if (score is null)
        {
            return;
        }

        score.Points = pointsViewModel.Points.ToString(CultureInfo.InvariantCulture);

        this.StateHasChanged();
    }
}
