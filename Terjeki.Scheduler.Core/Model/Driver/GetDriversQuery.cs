namespace Terjeki.Scheduler.Core
{
    public record GetDriversQuery() : IRequest<IEnumerable<DriverModel>>;
}
