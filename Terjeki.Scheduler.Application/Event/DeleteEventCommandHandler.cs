namespace Terjeki.Scheduler.Application
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, bool>
    {
        private readonly IMockDatabase _mockDatabase;
        public DeleteEventCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<bool> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {

            return await _mockDatabase.DeleteEvent(request.Id, cancellationToken);
        }
    }
}
