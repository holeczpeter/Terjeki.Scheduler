
namespace Terjeki.Scheduler.Web.Services
{
    public interface IAllowedEmailService
    {
        Task<AllowedEmailModel> CreateAsync(CreateAllowedEmailCommand command, CancellationToken cancellationToken = default);
        Task<AllowedEmailModel> UpdateAsync(UpdateAllowedEmailCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(DeleteAllowedEmailCommand command, CancellationToken cancellationToken = default);
        Task<IEnumerable<AllowedEmailModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<AllowedEmailModel> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<RoleModel>> GetRolesAsync(CancellationToken cancellationToken = default);
    }
}