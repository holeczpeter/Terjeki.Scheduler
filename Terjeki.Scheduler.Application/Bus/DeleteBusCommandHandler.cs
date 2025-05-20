namespace Terjeki.Scheduler.Application.Bus
{
    internal class DeleteBusCommandHandler : IRequestHandler<DeleteBusCommand, bool>
    {
        private readonly IMockDatabase _mockDatabase;
        public DeleteBusCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<bool> Handle(DeleteBusCommand request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.DeleteBus(request.Id, cancellationToken);
        }
    }
}
