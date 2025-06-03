namespace Terjeki.Scheduler.Application
{
    internal class GetAllowedEmailsQueryHandler : IRequestHandler<GetAllowedEmailsQuery, IEnumerable<AllowedEmailModel>>
    {
        private readonly AppDbContext _dbContext;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        public GetAllowedEmailsQueryHandler(AppDbContext dbContext, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
        }
        public async Task<IEnumerable<AllowedEmailModel>> Handle(GetAllowedEmailsQuery request, CancellationToken cancellationToken)
        {
            var currentRoles = await _roleManager.Roles
                .Select(r => new RoleModel
                {
                    Id = r.Id,
                    Name = r.Name!
                })
                .ToListAsync(cancellationToken);

            var allowedEmails = await _dbContext.AllowedEmails
                                .Where(x => x.EntityStatus == EntityStatuses.Active)
                                .Select(x => new
                                {
                                    x.Id,
                                    x.Email,
                                    RoleName = x.RoleName
                                })
                                .ToListAsync(cancellationToken);

            var result = allowedEmails
                        .Select(x =>
                        {
                
                            var roleModel = currentRoles
                                .FirstOrDefault(r => r.Name == x.RoleName);
                            return new AllowedEmailModel
                            {
                                Id = x.Id,
                                Email = x.Email,
                                Role = roleModel 
                            };
                        })
                        .ToList();

            return result;
        }
    }
}
