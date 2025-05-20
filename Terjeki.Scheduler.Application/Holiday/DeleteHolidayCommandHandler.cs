namespace Terjeki.Scheduler.Application.Holiday
{
    public class DeleteHolidayCommandHandler : IRequestHandler<DeleteHolidayCommand, bool>
    {
        private readonly IMockDatabase _mockDatabase;
        public DeleteHolidayCommandHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<bool> Handle(DeleteHolidayCommand request, CancellationToken cancellationToken)
        {

            return await _mockDatabase.DeleteHoliday(request.Id, cancellationToken);
        }
    }
}
