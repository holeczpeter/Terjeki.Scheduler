namespace Terjeki.Scheduler.Core
{
    public record DeleteHolidayCommand(Guid Id) : IRequest<bool>;
}
