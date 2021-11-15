namespace PointingPoker.Razor.Services;

public class SessionService
{
    private readonly HttpClient httpClient;

    public SessionService(HttpClient httpClient)
    {
        this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }


}
