using Microsoft.JSInterop;

namespace Terjeki.Scheduler.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;

        public AuthService(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }

        private const string TokenKey = "authToken";

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var resp = await _http.PostAsJsonAsync("api/account/register", dto);
            resp.EnsureSuccessStatusCode();
            return "Registration OK — check your email.";
        }

        public async Task LoginAsync(LoginDto dto)
        {
            var resp = await _http.PostAsJsonAsync("api/account/login", dto);
            resp.EnsureSuccessStatusCode();
            var data = await resp.Content.ReadFromJsonAsync<LoginResult>();
            await _js.InvokeVoidAsync("localStorage.setItem", TokenKey, data.Token);
        }

        public async Task StartTwoFactorAsync()
        {
            var resp = await _http.GetAsync("api/account/2fa/setup");
            resp.EnsureSuccessStatusCode();
            var data = await resp.Content.ReadFromJsonAsync<Setup2FaResult>();
            // return QR-uri + secret to the caller
            // eg. bind to a component
        }

        public async Task EnableTwoFactorAsync(Enable2FaDto dto)
        {
            var resp = await _http.PostAsJsonAsync("api/account/2fa/enable", dto);
            resp.EnsureSuccessStatusCode();
        }

        public async Task LoginWith2FaAsync(LoginWith2FaDto dto)
        {
            var resp = await _http.PostAsJsonAsync("api/account/login/2fa", dto);
            resp.EnsureSuccessStatusCode();
            var data = await resp.Content.ReadFromJsonAsync<LoginResult>();
            await _js.InvokeVoidAsync("localStorage.setItem", TokenKey, data.Token);
        }

        public async Task LogoutAsync()
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _js.InvokeAsync<string?>("localStorage.getItem", TokenKey);
        }

        class LoginResult { public string Token { get; set; } = default!; }
        class Setup2FaResult { public string QrUri { get; set; } = default!; public string Secret { get; set; } = default!; }
    }
}
