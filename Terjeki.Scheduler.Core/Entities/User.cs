using Microsoft.AspNetCore.Identity;

namespace Terjeki.Scheduler.Core.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = default!;
        
    }
}
