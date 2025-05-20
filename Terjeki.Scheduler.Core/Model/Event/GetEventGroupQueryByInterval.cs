namespace Terjeki.Scheduler.Core
{
    public record GetEventGroupQueryByInterval(DateTime Start, DateTime End) : IRequest<IEnumerable<EventGroupModel>>;
   
}
