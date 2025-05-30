using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Terjeki.Scheduler.Web.Services
{
    public class JwtAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _js;
        private readonly HttpClient _http;
        private const string TokenKey = "authToken";

        public JwtAuthenticationStateProvider(IJSRuntime js, HttpClient http)
        {
            _js = js;
            _http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _js.InvokeAsync<string>("localStorage.getItem", TokenKey);
            var identity = string.IsNullOrWhiteSpace(token)
                ? new ClaimsIdentity()
                : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

            _http.DefaultRequestHeaders.Authorization =
                identity.IsAuthenticated
                    ? new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token)
                    : null!;

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        public void NotifyUserAuthentication(string token)
            => NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        public void NotifyUserLogout()
            => NotifyAuthenticationStateChanged(Task.FromResult(
                new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
        
        public ValueTask<string> GetTokenAsync()
        {
            return _js.InvokeAsync<string>("localStorage.getItem", "authToken");
        }
        private static Claim[] ParseClaimsFromJwt(string jwt)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwt);
            return token.Claims.ToArray();
        }
    }
}
