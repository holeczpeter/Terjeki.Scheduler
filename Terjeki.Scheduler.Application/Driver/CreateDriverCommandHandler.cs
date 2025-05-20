namespace Terjeki.Scheduler.Application.Driver
{
    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, DriverModel>
    {
        private readonly IMockDatabase _mockDatabase;
        public CreateDriverCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<DriverModel> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {

            return await _mockDatabase.CreateDriver(request, cancellationToken);
        }
    }
}
