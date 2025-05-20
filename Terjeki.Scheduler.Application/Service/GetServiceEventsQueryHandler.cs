namespace Terjeki.Scheduler.Application.Event
{
    public class GetServiceEventsQueryHandler : IRequestHandler<GetServiceEventsQuery, IEnumerable<EventModel>>
    {
        private readonly IMockDatabase _mockDatabase;
        public GetServiceEventsQueryHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<IEnumerable<EventModel>> Handle(GetServiceEventsQuery request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.GetServiceEvents(cancellationToken);
        }
    }
}
