
namespace Terjeki.Scheduler.Web.Services
{
    public interface ICapacityService
    {
        Task<IEnumerable<CapacityModel>> GetAll(CancellationToken cancellationToken = default);
    }
}