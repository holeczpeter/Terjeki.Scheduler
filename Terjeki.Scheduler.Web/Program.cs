using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddRadzenComponents();


builder.Services.AddScoped<JwtAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<JwtAuthenticationStateProvider>());

builder.Services.AddTransient<AuthMessageHandler>();

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5227/")
    });

builder.Services.AddAuthorizationCore();


// 9) Alkalmazás-specifikus service-ek
builder.Services.AddApplicationServices();
builder.Services.AddWebApplicationServices();

await builder.Build().RunAsync();