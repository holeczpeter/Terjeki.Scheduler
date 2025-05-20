namespace Terjeki.Scheduler.Application.Event
{
    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, IEnumerable<EventModel>>
    {
        private readonly IMockDatabase _mockDatabase;
        public GetEventsQueryHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<IEnumerable<EventModel>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.GetEvents(cancellationToken);
        }
    }
}
