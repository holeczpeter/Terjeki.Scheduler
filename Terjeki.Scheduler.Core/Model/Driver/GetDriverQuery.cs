namespace Terjeki.Scheduler.Core
{
    public record GetDriverQuery(Guid Id) : IRequest<DriverModel>;
}
