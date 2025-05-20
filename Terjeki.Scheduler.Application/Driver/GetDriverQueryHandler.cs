namespace Terjeki.Scheduler.Application.Driver
{
    public class GetDriverQueryHandler : IRequestHandler<GetDriverQuery, DriverModel>
    {
        private readonly IMockDatabase _mockDatabase;
        public GetDriverQueryHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<DriverModel> Handle(GetDriverQuery request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.GetDriver(request.Id, cancellationToken);
        }
    }
}
