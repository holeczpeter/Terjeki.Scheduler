var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddRadzenComponents();
builder.Services
    .AddHttpClient("ApiClient", client =>
    {
        // Teszteléshez:
        client.BaseAddress = new Uri("http://localhost:5227/");

        // Élesben általában:
        // client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
        // (ha a Blazor is ugyanazon a szerveren fut, mint az API)
    });
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));
builder.Services.AddApplicationServices();
builder.Services.AddWebApplicationServices();
await builder.Build().RunAsync();
