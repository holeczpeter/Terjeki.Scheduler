﻿using System.Net.Http.Headers;

namespace Terjeki.Scheduler.Web.Services
{
   
    public class AuthMessageHandler : DelegatingHandler
    {
        private readonly JwtAuthenticationStateProvider _authStateProvider;

        public AuthMessageHandler(JwtAuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _authStateProvider.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
