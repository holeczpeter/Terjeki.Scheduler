namespace Terjeki.Scheduler.Application.Event
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, EventModel>
    {
        private readonly IMockDatabase _mockDatabase;
        public UpdateEventCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<EventModel> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {

            return await _mockDatabase.UpdateEvent(request, cancellationToken);
        }
    }
}
