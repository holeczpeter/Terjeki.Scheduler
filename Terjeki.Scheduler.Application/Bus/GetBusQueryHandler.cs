namespace Terjeki.Scheduler.Application.Bus
{
    public class GetBusQueryHandler : IRequestHandler<GetBusQuery, BusModel>
    {
        private readonly IMockDatabase _mockDatabase;
        public GetBusQueryHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<BusModel> Handle(GetBusQuery request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.GetBus(request.Id, cancellationToken);
        }
    }
}
