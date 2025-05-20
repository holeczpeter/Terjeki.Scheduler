namespace Terjeki.Scheduler.Core
{
    public record GetServiceEventsQuery(): IRequest<IEnumerable<EventModel>>;
    
}
