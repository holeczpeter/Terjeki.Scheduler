namespace Terjeki.Scheduler.Core.Entities
{
    public class Driver: BaseEntity
    {
        public string Name { get; set; }
        public Bus? Bus { get; set; }
        public Guid? UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public ICollection<DriverEvent> DriverEvents { get; set; } = new HashSet<DriverEvent>();
    }
}
