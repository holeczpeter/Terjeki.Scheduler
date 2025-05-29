namespace Terjeki.Scheduler.Application
{
    public class GetEventsQueryByIntervalHandler : IRequestHandler<GetEventsQueryByInterval, IEnumerable<EventModel>>
    {
        
        private readonly IMockDatabase _mockDatabase;
        public GetEventsQueryByIntervalHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<IEnumerable<EventModel>> Handle(GetEventsQueryByInterval request, CancellationToken cancellationToken)
        {
            
            
            return await _mockDatabase.GetEvents(request.Start,request.End,cancellationToken);
        }
    }
}
