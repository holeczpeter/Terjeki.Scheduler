namespace Terjeki.Scheduler.Application.Holiday
{
    public class CreateHolidayCommandHandler : IRequestHandler<CreateHolidayCommand, EventModel>
    {
        private readonly IMockDatabase _mockDatabase;
        public CreateHolidayCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<EventModel> Handle(CreateHolidayCommand request, CancellationToken cancellationToken)
        {

            return await _mockDatabase.CreateHoliday(request, cancellationToken);
        }
    }
}
