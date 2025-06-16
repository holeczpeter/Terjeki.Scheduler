using Microsoft.AspNetCore.Components.Authorization;

namespace Terjeki.Scheduler.Web
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddSingleton<IViewService, ViewService>();
            services.AddScoped<IAllowedEmailService, AllowedEmailService>();
            services.AddScoped<IDriverService, DriverService>();
            services.AddScoped<IBusService, BusService>();
            services.AddScoped<ICapacityService, CapacityService>();
            services.AddScoped<IHolidayService, HolidayService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IEventService, EventService>();
          
            services.AddAuthorizationCore();
            return services;
        }
    }
}
