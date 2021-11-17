using System.Text;
using System.Text.Json;
using PointingPoker.Common.Results;
using PointingPoker.Models;
using PointingPoker.Razor.ViewModels;

namespace PointingPoker.Razor.Services;

public class PokerSessionService : IPokerSessionService
{
    private const string JsonMediaType = "application/json";
    private const string SessionApiRoute = "api/v1/sessions";

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient httpClient;

    public PokerSessionService(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<Result<Guid>> CreateAsync(CreateSessionModel sessionModel)
    {
        using var sessionJson = new StringContent(JsonSerializer.Serialize(sessionModel), Encoding.UTF8, JsonMediaType);
        var response = await this.httpClient.PostAsync(SessionApiRoute, sessionJson).ConfigureAwait(false);

        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var session = JsonSerializer.Deserialize<SessionDto>(content, JsonSerializerOptions) ??
            throw new InvalidOperationException("Could not deserialize Session");

        return response.IsSuccessStatusCode ?
            Result.Ok(session.Id) :
            Result.Fail<Guid>(response.StatusCode.ToString());
    }

    public async Task<Result<SessionViewModel>> GetSessionWithPlayersAsync(Guid sessionId)
    {
        var response = await this.httpClient.GetAsync($"{SessionApiRoute}/{sessionId}").ConfigureAwait(false);
        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        var session = JsonSerializer.Deserialize<SessionWithPlayersDto>(content, JsonSerializerOptions) ??
            throw new InvalidOperationException("Could not deserialize Session");

        return response.IsSuccessStatusCode ?
            Result.Ok(session.AsViewModel()) :
            Result.Fail<SessionViewModel>(response.StatusCode.ToString());
    }
}
