namespace Terjeki.Scheduler.Core
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
