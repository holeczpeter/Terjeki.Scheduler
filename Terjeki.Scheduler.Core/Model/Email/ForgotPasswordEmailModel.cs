namespace Terjeki.Scheduler.Core
{
    public class ForgotPasswordEmailModel
    {
        public string UserName { get; set; } = string.Empty;
        public string ResetLink { get; set; } = string.Empty;
        public int ExpirationMinutes { get; set; }
        public string SupportEmail { get; set; } = "support@terjeki-scheduler.hu";
        public string CompanyName { get; set; } = "Terjéki Naptár csapata";
    }
}
