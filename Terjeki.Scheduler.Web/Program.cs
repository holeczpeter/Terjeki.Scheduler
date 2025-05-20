

using Microsoft.AspNetCore.Components.Authorization;
using Terjeki.Scheduler.Core;
using Terjeki.Scheduler.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddRadzenComponents();
builder.Services.AddApplicationServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IViewService, ViewService>();
builder.Services.AddSingleton<TerjekiAuthenticationStateProvider>();
builder.Services.AddSingleton<AuthenticationStateProvider>(sp => sp.GetRequiredService<TerjekiAuthenticationStateProvider>());
builder.Services.AddAuthorizationCore();
await builder.Build().RunAsync();
