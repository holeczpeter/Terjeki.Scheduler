namespace Terjeki.Scheduler.Core
{
    public class UpdateHolidayCommand : CreateHolidayCommand, IRequest<EventModel>
    {
        public Guid Id { get; set; }
    }
}
