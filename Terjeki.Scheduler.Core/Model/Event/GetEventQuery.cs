namespace Terjeki.Scheduler.Core
{
    public record GetEventQuery(Guid Id) : IRequest<EventModel>;
}
