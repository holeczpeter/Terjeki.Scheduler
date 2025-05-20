namespace Terjeki.Scheduler.Core
{
    public class UpdateEventCommand : CreateEventCommand, IRequest<EventModel>
    {
        public Guid Id { get; set; }
    }
}

