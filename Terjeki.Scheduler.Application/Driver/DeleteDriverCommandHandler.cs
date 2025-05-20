namespace Terjeki.Scheduler.Application.Driver
{
    public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, bool>
    {
        private readonly IMockDatabase _mockDatabase;
        public DeleteDriverCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<bool> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
        {

            return await _mockDatabase.DeleteDriver(request.Id, cancellationToken);
        }
    }
}
