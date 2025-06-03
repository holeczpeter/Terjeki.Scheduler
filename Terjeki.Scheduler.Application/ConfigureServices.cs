

using Microsoft.AspNetCore.Components.Authorization;

namespace Terjeki.Scheduler.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            
            services.AddSingleton<TerjekiAuthenticationStateProvider>();
            services.AddSingleton<AuthenticationStateProvider>(sp => sp.GetRequiredService<TerjekiAuthenticationStateProvider>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            
            return services;
        }
    }
}
