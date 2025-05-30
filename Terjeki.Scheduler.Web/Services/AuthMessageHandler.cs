using System.Net.Http.Headers;

namespace Terjeki.Scheduler.Web.Services
{
   
    public class AuthMessageHandler : DelegatingHandler
    {
        private readonly JwtAuthenticationStateProvider _authState;

        public AuthMessageHandler(JwtAuthenticationStateProvider authState)
        {
            _authState = authState;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await _authState.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
