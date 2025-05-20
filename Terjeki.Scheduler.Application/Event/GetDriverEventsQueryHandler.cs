namespace Terjeki.Scheduler.Application.Event
{
    public class GetDriverEventsQueryHandler : IRequestHandler<GetDriverEventsQueryByInterval, IEnumerable<DriverEventModel>>
    {
        private readonly IMockDatabase _mockDatabase;
        public GetDriverEventsQueryHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<IEnumerable<DriverEventModel>> Handle(GetDriverEventsQueryByInterval request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.GetDriverEvents(request.Start, request.End, cancellationToken);
        }
    }
}
