namespace Terjeki.Scheduler.Application.Bus
{
    public class GetBusesQueryHandler : IRequestHandler<GetBusesQuery, IEnumerable<BusModel>>
    {
        private readonly IMockDatabase _mockDatabase;
        public GetBusesQueryHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<IEnumerable<BusModel>> Handle(GetBusesQuery request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.GetBuses(cancellationToken);
        }
    }
}
