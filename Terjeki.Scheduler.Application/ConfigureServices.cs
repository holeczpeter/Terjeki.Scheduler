

using Microsoft.AspNetCore.Components.Authorization;

namespace Terjeki.Scheduler.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IMockDatabase, MockDatabase>();
            services.AddSingleton<TerjekiAuthenticationStateProvider>();
            services.AddSingleton<AuthenticationStateProvider>(sp => sp.GetRequiredService<TerjekiAuthenticationStateProvider>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddSingleton<IMockDatabase, MockDatabase>();
            return services;
        }
    }
}
