
namespace Terjeki.Scheduler.Web.Services
{
    public interface IServiceService
    {
        Task<EventModel> Create(CreateServiceCommand command, CancellationToken cancellationToken = default);
        Task<EventModel> Update(UpdateServiceCommand command, CancellationToken cancellationToken = default);
        Task<bool> Delete(DeleteEventCommand command, CancellationToken cancellationToken = default);
        Task<IEnumerable<EventModel>> GetAll(CancellationToken cancellationToken = default);
        Task<IEnumerable<ServiceModel>> GetAllTypesAsync(CancellationToken cancellationToken = default);
    }
}