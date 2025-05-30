using Terjeki.Scheduler.Core.Entities;

namespace Terjeki.Scheduler.Core
{
    public class AllowedEmail : BaseEntity
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        public bool IsUsed { get; set; } = false;

        [Required]
        public string RoleName { get; set; } = null!;
    }
}
