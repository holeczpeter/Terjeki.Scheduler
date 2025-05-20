namespace Terjeki.Scheduler.Core
{
    public class CreateEventCommand : IRequest<EventModel>
    {
        public CapacityModel Capacity { get; set; }
        public BusModel Bus { get; set; }

        public List<DriverModel> Drivers { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public EventStatuses Status { get; set; }

        public EventTypes Type { get; set; }

        public ServiceTypes ServiceType { get; set; }
    }
}
