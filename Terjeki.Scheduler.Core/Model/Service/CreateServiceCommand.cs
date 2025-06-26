namespace Terjeki.Scheduler.Core
{
    public class CreateServiceCommand : IRequest<EventModel>
    {
        
        public Guid BusId { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public EventStatuses Status { get; set; }

        public EventTypes Type { get; set; } = EventTypes.Service;

        public ServiceTypes ServiceType { get; set; }
        public int CurrentMileage { get; set; }
        public string Summary { get; set; }
    }
}
