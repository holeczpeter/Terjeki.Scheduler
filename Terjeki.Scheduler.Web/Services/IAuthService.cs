
namespace Terjeki.Scheduler.Web.Services
{
    public interface IAuthService
    {
        Task EnableTwoFactorAsync(Enable2FaDto dto);
        Task<string?> GetTokenAsync();
        Task LoginAsync(LoginDto dto);
        Task LoginWith2FaAsync(LoginWith2FaDto dto);
        Task LogoutAsync();
        Task<string> RegisterAsync(RegisterDto dto);
        Task StartTwoFactorAsync();
    }
}