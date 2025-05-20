namespace Terjeki.Scheduler.Core
{
    public record GetEventsQueryByInterval(DateTime Start, DateTime End) : IRequest<IEnumerable<EventModel>>;
}
