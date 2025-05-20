namespace Terjeki.Scheduler.Application.Event
{
    public class GetEventQueryHandler : IRequestHandler<GetEventQuery, EventModel>
    {
        private readonly IMockDatabase _mockDatabase;
        public GetEventQueryHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<EventModel> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.GetEvent(request.Id, cancellationToken);
        }
    }
}
