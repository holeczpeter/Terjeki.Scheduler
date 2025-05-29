namespace Terjeki.Scheduler.Core.Entities
{
    
    public class DriverEvent : BaseEntity
    {
        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public Guid DriverId { get; set; }
        public Driver Driver { get; set; }

    }
}
