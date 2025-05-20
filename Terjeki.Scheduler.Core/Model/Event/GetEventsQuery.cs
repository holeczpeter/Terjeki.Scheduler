namespace Terjeki.Scheduler.Core
{
    public record GetEventsQuery() : IRequest<IEnumerable<EventModel>>;
}
