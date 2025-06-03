namespace Terjeki.Scheduler.Application
{
    internal class GetAllowedEmailQueryHandler : IRequestHandler<GetAllowedEmailQuery, AllowedEmailModel>
    {
        private readonly AppDbContext _dbContext;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        public GetAllowedEmailQueryHandler(AppDbContext dbContext, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        
        
        public async Task<AllowedEmailModel> Handle(GetAllowedEmailQuery request, CancellationToken cancellationToken)
        {
            var currentRole = await _roleManager.Roles
                .Select(r => new RoleModel
                {
                    Id = r.Id,
                    Name = r.Name!
                })
                .FirstOrDefaultAsync(cancellationToken);
            return await _dbContext.AllowedEmails.Where(x => x.Id == request.Id).Select(x => new AllowedEmailModel()
            {
                Id = x.Id,
                Email = x.Email,
                Role = new RoleModel
                {
                    Id = currentRole.Id,
                    Name = currentRole.Name!
                }
            }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
