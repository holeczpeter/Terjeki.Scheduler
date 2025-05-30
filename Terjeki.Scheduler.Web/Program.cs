using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// 1) Root komponensek
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 2) Radzen (ha kell)
builder.Services.AddRadzenComponents();

// majd a megszokott módon felülírjuk az absztrakt Binding-et
builder.Services.AddScoped<AuthenticationStateProvider>(sp =>
    sp.GetRequiredService<JwtAuthenticationStateProvider>()
);
// 2) Az AuthService, ami belövés után meghívja NotifyUserAuthentication-t
builder.Services.AddScoped<IAuthService, AuthService>();

// 3) A BearerTokenHandler
builder.Services.AddTransient<AuthMessageHandler>();

// 4) HttpClient, ami minden híváskor beteszi a tokent
builder.Services
    .AddHttpClient("ApiClient", client =>
    {
        client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
    })
    .AddHttpMessageHandler<AuthMessageHandler>();

// 5) Ha injektáltatod `HttpClient`-ként:
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));

// 6) Szabályozás Blazorban
builder.Services.AddAuthorizationCore();

// 9) Alkalmazás-specifikus service-ek
builder.Services.AddApplicationServices();
builder.Services.AddWebApplicationServices();

await builder.Build().RunAsync();