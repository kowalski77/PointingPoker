using System.Text.Json;
using PointingPoker.Common.Results;
using PointingPoker.Models;

namespace PointingPoker.Razor.Services;

public class SessionService : ISessionService
{
    private const string SessionApiRoute = "api/v1/sessions";
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient httpClient;

    public SessionService(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<Result<SessionDto>> CreateSessionAsync()
    {
        var response = await this.httpClient.PostAsync(SessionApiRoute, null).ConfigureAwait(false);

        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var session = JsonSerializer.Deserialize<SessionDto>(content, JsonSerializerOptions) ??
            throw new InvalidOperationException("Could not deserialize Session");

        return response.IsSuccessStatusCode ?
            Result.Ok(session) :
            Result.Fail<SessionDto>(response.StatusCode.ToString());
    }
}
