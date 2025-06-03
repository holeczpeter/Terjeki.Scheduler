namespace Terjeki.Scheduler.Application.User
{
    public class GetAllDriversQueryHandler : IRequestHandler<GetAllDriversQuery, IEnumerable<UserModel>>
    {
        private readonly AppDbContext _dbContext;

        public GetAllDriversQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
       
        public async Task<IEnumerable<UserModel>> Handle(GetAllDriversQuery request, CancellationToken cancellationToken)
        {
            var existingDriverUserIds = await _dbContext.Drivers
               .Where(d => d.UserId != null && d.EntityStatus == EntityStatuses.Active)
               .Select(d => d.UserId!.Value)
               .ToListAsync(cancellationToken);

           
            var result = await _dbContext.Users.Where(u=> !existingDriverUserIds.Contains(u.Id)).Select(u => new UserModel
            {
                Id = u.Id,
                FullName = u.FullName ?? u.UserName ?? string.Empty,
                Email = u.Email ?? string.Empty
            }).ToListAsync(cancellationToken);

            return result;
        }
    }
}
