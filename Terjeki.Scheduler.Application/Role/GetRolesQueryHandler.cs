namespace Terjeki.Scheduler.Application
{
    internal class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<RoleModel>>
    {
        public RoleModel AdminRole = new RoleModel
        {
            Id = Guid.NewGuid(),
            Name = "Adminisztrátor",
            Type = RoleTypes.Admin
        };

        public RoleModel DriverRole = new RoleModel
        {
            Id = Guid.NewGuid(),
            Name = "Sofőr",
            Type = RoleTypes.Driver
        };

        public RoleModel ServiceRole = new RoleModel
        {
            Id = Guid.NewGuid(),
            Name = "Szerelő",
            Type = RoleTypes.Service
        };
        public GetRolesQueryHandler()
        {
            
        }
        public async Task<IEnumerable<RoleModel>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = new List<RoleModel>
                    {
                        AdminRole,
                        DriverRole,
                        ServiceRole
                    };

            return roles;
        }
    }
}
