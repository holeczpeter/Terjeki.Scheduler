namespace Terjeki.Scheduler.Application.Capacities
{
    public class GetCapacitiesQueryHandler : IRequestHandler<GetCapacities, IEnumerable<CapacityModel>>
    {
        private readonly IMockDatabase _mockDatabase;
        public GetCapacitiesQueryHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<IEnumerable<CapacityModel>> Handle(GetCapacities request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.GetCapacities(cancellationToken);
        }
    }
}
