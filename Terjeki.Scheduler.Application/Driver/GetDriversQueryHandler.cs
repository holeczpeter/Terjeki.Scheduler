namespace Terjeki.Scheduler.Application.Driver
{
    public class GetDriversQueryHandler : IRequestHandler<GetDriversQuery, IEnumerable<DriverModel>>
    {
        private readonly IMockDatabase _mockDatabase;
        public GetDriversQueryHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<IEnumerable<DriverModel>> Handle(GetDriversQuery request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.GetDrivers(cancellationToken);
        }
    }
}
