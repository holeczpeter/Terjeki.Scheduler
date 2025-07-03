namespace Terjeki.Scheduler.Core
{
    public class CreateEventCommand : IRequest<EventModel>
    {
        public int Capacity { get; set; }
        public Guid? BusId { get; set; }

        public List<Guid> DriverIds { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public EventStatuses Status { get; set; }

        public EventTypes Type { get; set; }

        public ServiceTypes ServiceType { get; set; }
        public string Summary { get; set; }
        public bool IsNotification { get; set; }
    }
}
