namespace Terjeki.Scheduler.Core
{
    public record ResetPasswordDto(string Email, string Token, string NewPassword);
}
