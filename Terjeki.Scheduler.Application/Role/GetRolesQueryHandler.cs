

namespace Terjeki.Scheduler.Application
{
    internal class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<RoleModel>>
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        public GetRolesQueryHandler(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IEnumerable<RoleModel>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleManager.Roles
                .Select(r => new RoleModel
                {
                    Id = r.Id,
                    Name = r.Name!
                })
                .ToListAsync(cancellationToken);

            return roles;
        }
    }
}
