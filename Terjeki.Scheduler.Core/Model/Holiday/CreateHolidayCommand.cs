namespace Terjeki.Scheduler.Core
{
    public class CreateHolidayCommand : IRequest<EventModel>
    {
        public DriverModel Driver { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public EventTypes Type { get; set; }
        public HolidayTypes HolidayType { get; set; }
    }
}
