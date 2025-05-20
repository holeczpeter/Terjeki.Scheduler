namespace Terjeki.Scheduler.Core
{
    public record GetHolidayTypesQuery() : IRequest<IEnumerable<HolidayModel>>;
}
