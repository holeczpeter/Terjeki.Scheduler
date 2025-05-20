namespace Terjeki.Scheduler.Core
{
    public record GetUsersQuery() : IRequest<IEnumerable<UserModel>>;
}
