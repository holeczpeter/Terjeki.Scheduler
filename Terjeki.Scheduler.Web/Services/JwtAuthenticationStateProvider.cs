using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

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

            Console.WriteLine($"[AUTH] Token: {token}");

            if (string.IsNullOrWhiteSpace(token))
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            var claims = ParseClaimsFromJwt(token);
            foreach (var claim in claims)
            {
                Console.WriteLine($"[AUTH] Claim: {claim.Type} = {claim.Value}");
            }

            var identity = string.IsNullOrWhiteSpace(token)
                ? new ClaimsIdentity()
                : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
            var principal = new ClaimsPrincipal(identity);
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
        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var claims = new List<Claim>();
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            foreach (var kvp in keyValuePairs)
            {
                // Ha a role claim egyes szám
                if (kvp.Key == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                {
                    if (kvp.Value is JsonElement element && element.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var role in element.EnumerateArray())
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.GetString()!));
                        }
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, kvp.Value.ToString()!));
                    }
                    continue;
                }
                // ha a role claim a "role" vagy "roles" kulcson jönne
                if (kvp.Key == "role" || kvp.Key == "roles")
                {
                    if (kvp.Value is JsonElement element && element.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var role in element.EnumerateArray())
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.GetString()!));
                        }
                    }
                    else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, kvp.Value.ToString()!));
                    }
                    continue;
                }
                // egyéb claim
                claims.Add(new Claim(kvp.Key, kvp.Value.ToString()!));
            }
            return claims;
        }
        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            // JWT base64Url: "-" helyett "+", "_" helyett "/"
            base64 = base64.Replace('-', '+').Replace('_', '/');
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

    }
}
