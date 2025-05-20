namespace Terjeki.Scheduler.Core
{
    public record GetDriverEventsQueryByInterval(DateTime Start, DateTime End) : IRequest<IEnumerable<DriverEventModel>>;
}
