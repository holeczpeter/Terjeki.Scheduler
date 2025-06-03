namespace Terjeki.Scheduler.Core
{
    public class AllowedEmailModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public RoleModel Role { get; set; }
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
