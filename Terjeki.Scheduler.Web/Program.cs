using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Terjeki.Scheduler.Web.Pages;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddRadzenComponents();


builder.Services.AddScoped<JwtAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<JwtAuthenticationStateProvider>());

builder.Services.AddTransient<AuthMessageHandler>();
builder.Services.AddTransient<UnauthorizedHttpHandler>(sp =>
{
    var navigationManager = sp.GetRequiredService<Microsoft.AspNetCore.Components.NavigationManager>();
    var logger = sp.GetRequiredService<ILogger<UnauthorizedHttpHandler>>();
    return new UnauthorizedHttpHandler(navigationManager, logger);
});

builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("http://localhost:5227/");
})
.AddHttpMessageHandler<AuthMessageHandler>()
.AddHttpMessageHandler<UnauthorizedHttpHandler>();

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));

builder.Services.AddAuthorizationCore();
builder.Services.AddApplicationServices();
builder.Services.AddWebApplicationServices();

await builder.Build().RunAsync();