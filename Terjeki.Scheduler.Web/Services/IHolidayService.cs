
namespace Terjeki.Scheduler.Web.Services
{
    public interface IHolidayService
    {
        Task<EventModel> CreateAsync(CreateHolidayCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(DeleteHolidayCommand command, CancellationToken cancellationToken = default);
        Task<IEnumerable<BusModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<HolidayModel>> GetAllTypesAsync(CancellationToken cancellationToken = default);
        Task<BusModel> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<EventModel> UpdateAsync(UpdateHolidayCommand command, CancellationToken cancellationToken = default);
    }
}