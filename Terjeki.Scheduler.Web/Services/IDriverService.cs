
namespace Terjeki.Scheduler.Web.Services
{
    public interface IDriverService
    {
        Task<DriverModel> Create(CreateDriverCommand command, CancellationToken cancellationToken = default);
        Task<bool> Delete(DeleteDriverCommand command, CancellationToken cancellationToken = default);
        Task<DriverModel> Get(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<DriverModel>> GetAll(CancellationToken cancellationToken = default);
        Task<DriverModel> Update(UpdateDriverCommand command, CancellationToken cancellationToken = default);
    }
}