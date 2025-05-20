namespace Terjeki.Scheduler.Application.Bus
{
    public class CreateBusCommandHandler : IRequestHandler<CreateBusCommand, BusModel>
    {
        private readonly IMockDatabase _mockDatabase;
        public CreateBusCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<BusModel> Handle(CreateBusCommand request, CancellationToken cancellationToken)
        {

            return await _mockDatabase.CreateBus(request, cancellationToken);    
        }
    }
}
