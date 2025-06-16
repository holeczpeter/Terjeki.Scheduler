using System.Net.Http.Headers;

namespace Terjeki.Scheduler.Web.Services
{

    public class AuthMessageHandler : DelegatingHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public AuthMessageHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var authStateProvider = _serviceProvider.GetRequiredService<IAuthService>();
            var token = await authStateProvider.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
