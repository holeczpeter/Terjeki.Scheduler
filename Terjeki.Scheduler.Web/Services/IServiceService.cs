
namespace Terjeki.Scheduler.Web.Services
{
    public interface IServiceService
    {
        Task<IEnumerable<EventModel>> GetAll(CancellationToken cancellationToken = default);
        Task<IEnumerable<ServiceModel>> GetAllTypesAsync(CancellationToken cancellationToken = default);
    }
}