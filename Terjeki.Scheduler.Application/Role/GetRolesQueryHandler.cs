namespace Terjeki.Scheduler.Application.Role
{
    internal class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, IEnumerable<RoleModel>>
    {
        private readonly IMockDatabase _mockDatabase;
        public GetRolesQueryHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<IEnumerable<RoleModel>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.GetRoles(cancellationToken);
        }
    }
}
