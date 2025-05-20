namespace Terjeki.Scheduler.Core
{
    public record GetBusesQuery() : IRequest<IEnumerable<BusModel>>;
}
