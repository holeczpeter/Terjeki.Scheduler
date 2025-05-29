
namespace Terjeki.Scheduler.Web.Services
{
    public interface IBusService
    {
        Task<BusModel> Create(CreateBusCommand command, CancellationToken cancellationToken = default);
        Task<bool> Delete(DeleteBusCommand command, CancellationToken cancellationToken = default);
        Task<BusModel> Get(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<BusModel>> GetAll(CancellationToken cancellationToken = default);
        Task<BusModel> Update(UpdateBusCommand command, CancellationToken cancellationToken = default);
    }
}