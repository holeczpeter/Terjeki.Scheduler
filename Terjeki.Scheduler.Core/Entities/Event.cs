namespace Terjeki.Scheduler.Core.Entities
{
    public class Event : BaseEntity
    {
        public Guid? BusId { get; set; }
        public Bus? Bus { get; set; }

        public ICollection<DriverEvent> DriverEvents { get; set; } = new HashSet<DriverEvent>();
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public EventStatuses Status { get; set; }

        public EventTypes Type { get; set; }

        public ServiceTypes ServiceType { get; set; }

        public HolidayTypes HolidayType { get; set; }
    }
}
