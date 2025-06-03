using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net;

namespace Terjeki.Scheduler.Web.Services
{
    public class UnauthorizedHttpHandler : DelegatingHandler
    {
        private readonly NavigationManager _navigationManager;
        private readonly ILogger<UnauthorizedHttpHandler> _logger;

        public UnauthorizedHttpHandler(
            NavigationManager navigationManager,
            ILogger<UnauthorizedHttpHandler> logger)
        {
            _navigationManager = navigationManager;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await base.SendAsync(request, cancellationToken);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    if (!_navigationManager.Uri.Contains("login"))
                    {
                        var requestPath = request.RequestUri?.AbsolutePath.ToLowerInvariant() ?? "";
                        if (!requestPath.Contains("/authentication/") && !requestPath.Contains("/connect/"))
                        {
                            _navigationManager.NavigateToLogout("login");
                        }
                    }
                }

                return response;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "API elérési hiba történt a következő címre irányuló kérés közben: {RequestUri}", request.RequestUri);

                var currentUri = _navigationManager.Uri;
                if (!currentUri.Contains("/api-unavailable"))
                {
                    _navigationManager.NavigateTo($"/api-unavailable?returnUrl={Uri.EscapeDataString(currentUri)}");
                }
                else
                {
                    _navigationManager.NavigateTo("/");
                }

                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                {
                    Content = new StringContent($"Az API ({request.RequestUri}) jelenleg nem elérhető. Navigálás hibaoldalra. Hiba: {ex.Message}"),
                    ReasonPhrase = "API Unavailable - Navigating to error page"
                };
            }
        }
    }
}
