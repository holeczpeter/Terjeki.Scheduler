namespace Terjeki.Scheduler.Core
{
    public record GetServiceTypesQuery() : IRequest<IEnumerable<ServiceModel>>;
}
