namespace Terjeki.Scheduler.Application.Bus
{
    public class UpdateBusCommandHandler : IRequestHandler<UpdateBusCommand, BusModel>
    {
        private readonly IMockDatabase _mockDatabase;
        public UpdateBusCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<BusModel> Handle(UpdateBusCommand request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.UpdateBus(request, cancellationToken);
        }
    }
}
