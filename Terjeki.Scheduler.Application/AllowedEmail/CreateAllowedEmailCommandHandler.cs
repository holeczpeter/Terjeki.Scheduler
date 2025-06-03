namespace Terjeki.Scheduler.Application
{

    public class CreateAllowedEmailCommandHandler : IRequestHandler<CreateAllowedEmailCommand, AllowedEmailModel>
    {
        private readonly AppDbContext _dbContext;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public CreateAllowedEmailCommandHandler(AppDbContext dbContext, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
        }

        public async Task<AllowedEmailModel> Handle(CreateAllowedEmailCommand request, CancellationToken cancellationToken)
        {

            var newAllowed = new AllowedEmail()
            {
                Id = new Guid(),
                Email = request.Email,  
                RoleName = request.Role.Name,
            };
            await _dbContext.AddAsync(newAllowed, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            var currentRole = await _roleManager.Roles
                .Select(r => new RoleModel
                {
                    Id = r.Id,
                    Name = r.Name!
                })
                .FirstOrDefaultAsync(cancellationToken);

            return await _dbContext.AllowedEmails.Where(x => x.Id == newAllowed.Id).Select(x => new AllowedEmailModel()
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
