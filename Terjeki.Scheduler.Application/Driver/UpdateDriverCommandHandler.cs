namespace Terjeki.Scheduler.Application.Driver
{
    internal class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, DriverModel>
    {
        private readonly IMockDatabase _mockDatabase;
        public UpdateDriverCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<DriverModel> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {

            return await _mockDatabase.UpdateDriver(request, cancellationToken);
        }
    }
}

