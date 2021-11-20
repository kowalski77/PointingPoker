using Blazored.SessionStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using PointingPoker.Razor.Hubs;
using PointingPoker.Razor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddBlazorise(options =>
{
    options.ChangeTextOnKeyPress = true;
    options.DelayTextOnKeyPress = true;
    options.DelayTextOnKeyPressInterval = 1000;
})
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Services.AddHttpClient<IPokerSessionService, PokerSessionService>(client => client.BaseAddress = new Uri("https://localhost:7047"));
builder.Services.AddBlazoredSessionStorage();

builder.Services.AddScoped<IGameConnectionHub, GameConnectionHub>();   

WebApplication? app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapHub<GameHub>(GameHub.HubUrl);

using var scope = app.Services.CreateScope();
var gameConnectionHub = scope.ServiceProvider.GetRequiredService<IGameConnectionHub>();
await gameConnectionHub.StartAsync().ConfigureAwait(false);

await app.RunAsync().ConfigureAwait(false);
