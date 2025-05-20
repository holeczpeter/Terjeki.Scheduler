namespace Terjeki.Scheduler.Core
{
    public record GetRolesQuery() : IRequest<IEnumerable<RoleModel>>;
   
}
