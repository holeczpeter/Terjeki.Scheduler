namespace Terjeki.Scheduler.Core.Entities
{
    public class Bus: BaseEntity
    {
        public Guid CapacityId { get; set; }
        public Capacity Capacity { get; set; }
        public Guid? DriverId { get; set; }
        public Driver? Driver { get; set; }
        public string Description { get; set; }
        public string LicensePlateNumber { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public int CurrentMileage { get; set; }
        public ICollection<Event> Events { get; set; } = new HashSet<Event>();
        
    }
}
