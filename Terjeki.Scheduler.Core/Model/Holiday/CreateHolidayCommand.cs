namespace Terjeki.Scheduler.Core
{
    public class CreateHolidayCommand : IRequest<EventModel>
    {
        public Guid DriverId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public EventTypes Type { get; set; }
        public HolidayTypes HolidayType { get; set; }
        public string Summary { get; set; }
    }
}
