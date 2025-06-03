
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Terjeki.Scheduler.Core
{
    public class TerjekiAuthenticationStateProvider : AuthenticationStateProvider
    {

        private ClaimsPrincipal _user = new ClaimsPrincipal(new ClaimsIdentity());

        public void MarkUserAsAuthenticated(AllowedEmailModel user)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role,  user.Role.Type.ToString())
            }, "Fake authentication");

            _user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void MarkUserAsLoggedOut()
        {
            _user = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_user));
        }
    }
}
