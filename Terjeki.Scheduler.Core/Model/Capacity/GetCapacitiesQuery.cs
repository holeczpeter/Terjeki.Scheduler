namespace Terjeki.Scheduler.Core
{
    public record GetCapacities() : IRequest<IEnumerable<CapacityModel>>;
}
