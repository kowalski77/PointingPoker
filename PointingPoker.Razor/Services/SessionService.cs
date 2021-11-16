using System.Text.Json;
using PointingPoker.Common.Results;

namespace PointingPoker.Razor.Services;

public class SessionService : ISessionService
{
    private const string SessionApiRoute = "api/v1/sessions";

    private readonly HttpClient httpClient;

    public SessionService(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<Result<int>> CreateSessionAsync()
    {
        var response = await this.httpClient.PostAsync(SessionApiRoute, null).ConfigureAwait(false);

        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        var sessionNumber = JsonSerializer.Deserialize<int>(content);

        return response.IsSuccessStatusCode ?
            Result.Ok(sessionNumber) :
            Result.Fail<int>(response.StatusCode.ToString());
    }
}
