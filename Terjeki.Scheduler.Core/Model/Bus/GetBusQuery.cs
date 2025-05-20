namespace Terjeki.Scheduler.Core
{
    public record GetBusQuery(Guid Id) : IRequest<BusModel>;
}
