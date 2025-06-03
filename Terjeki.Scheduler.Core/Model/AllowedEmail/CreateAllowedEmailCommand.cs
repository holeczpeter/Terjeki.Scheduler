namespace Terjeki.Scheduler.Core
{
    public class CreateAllowedEmailCommand : IRequest<AllowedEmailModel>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public RoleModel Role { get; set; }
    }
}
