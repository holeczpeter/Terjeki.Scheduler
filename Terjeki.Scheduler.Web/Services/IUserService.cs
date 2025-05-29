
namespace Terjeki.Scheduler.Web.Services
{
    public interface IUserService
    {
        Task<UserModel> CreateAsync(CreateUserCommand command, CancellationToken cancellationToken = default);
        Task<UserModel> UpdateAsync(UpdateUserCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(DeleteUserCommand command, CancellationToken cancellationToken = default);
        Task<IEnumerable<UserModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<UserModel> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<RoleModel>> GetRolesAsync(CancellationToken cancellationToken = default);
    }
}