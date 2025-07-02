using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddRadzenComponents();


builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
builder.Services.AddTransient<AuthMessageHandler>();
builder.Services.AddTransient<UnauthorizedHttpHandler>(sp =>
{
    var navigationManager = sp.GetRequiredService<Microsoft.AspNetCore.Components.NavigationManager>();
    var logger = sp.GetRequiredService<ILogger<UnauthorizedHttpHandler>>();
    return new UnauthorizedHttpHandler(navigationManager, logger);
});
var apiBaseUrl = builder.Configuration["ApiBaseUrl"]!;
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
})
.AddHttpMessageHandler<AuthMessageHandler>()
.AddHttpMessageHandler<UnauthorizedHttpHandler>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));

builder.Services.AddApplicationServices();
builder.Services.AddWebApplicationServices();

await builder.Build().RunAsync();