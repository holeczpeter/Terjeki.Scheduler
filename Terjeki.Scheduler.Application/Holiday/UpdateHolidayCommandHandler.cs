namespace Terjeki.Scheduler.Application.Holiday
{
    internal class UpdateHolidayCommandHandler : IRequestHandler<UpdateHolidayCommand, EventModel>
    {
        private readonly IMockDatabase _mockDatabase;
        public UpdateHolidayCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<EventModel> Handle(UpdateHolidayCommand request, CancellationToken cancellationToken)
        {

            return await _mockDatabase.UpdateHoliday(request, cancellationToken);
        }
    }
}
