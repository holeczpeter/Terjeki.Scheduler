namespace Terjeki.Scheduler.Application
{
    public class GetEventGroupQueryByIntervalHandler : IRequestHandler<GetEventGroupQueryByInterval, IEnumerable<EventGroupModel>>
    {
        private readonly IMockDatabase _mockDatabase;
        public GetEventGroupQueryByIntervalHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<IEnumerable<EventGroupModel>> Handle(GetEventGroupQueryByInterval request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.GetEventGroups(request.Start, request.End, cancellationToken);
        }
    }
}
