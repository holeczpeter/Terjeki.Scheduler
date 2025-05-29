namespace Terjeki.Scheduler.Application
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventModel>
    {
        private readonly IMockDatabase _mockDatabase;
        public CreateEventCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<EventModel> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {

            return await _mockDatabase.CreateEvent(request, cancellationToken);
        }
    }
}
