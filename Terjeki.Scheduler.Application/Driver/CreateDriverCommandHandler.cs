namespace Terjeki.Scheduler.Application
{
    public class CreateDriverCommandHandler : IRequestHandler<CreateDriverCommand, DriverModel>
    {
        private readonly AppDbContext _dbContext;

        public CreateDriverCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<DriverModel> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
        {
            
            var currentBus = await this._dbContext.Buses
               .Where(x => x.Id == request.BusId)
               .FirstOrDefaultAsync(cancellationToken);
            
            var currentUser = await _dbContext.Users
                .Where(x => x.Id == request.DriverUserId)
                .FirstOrDefaultAsync(cancellationToken);
            var isExistingDriver = await this._dbContext.Drivers
                    .Where(x => x.UserId != null && x.UserId == request.DriverUserId && x.EntityStatus == EntityStatuses.Deleted)
                    .AnyAsync(cancellationToken);
            if (isExistingDriver)
            {
                var existingDriver = await this._dbContext.Drivers
                    .Where(x => x.UserId != null && x.UserId == request.DriverUserId && x.EntityStatus == EntityStatuses.Deleted)
                    .FirstOrDefaultAsync(cancellationToken);
                if (existingDriver != null)
                {
                    existingDriver.EntityStatus = EntityStatuses.Active;
                    existingDriver.Bus = currentBus;
                }
                await this._dbContext.SaveChangesAsync(cancellationToken);
                return await GetResult(existingDriver.Id, cancellationToken);
            }
            else
            {
                var driver = new Driver()
                {
                    Id = Guid.NewGuid(),
                    Name = currentUser != null ? currentUser.FullName : request.DriverName,
                    Bus = currentBus,
                    UserId = request.DriverUserId
                };
                await this._dbContext.Drivers.AddAsync(driver, cancellationToken);
                await this._dbContext.SaveChangesAsync(cancellationToken);
                return await GetResult(driver.Id, cancellationToken);
            }
        }
        public async Task<DriverModel> GetResult(Guid id,CancellationToken cancellationToken)
        {
            return await this._dbContext.Drivers
                   .Where(x => x.Id == id)
                   .Select(x => new DriverModel
                   {
                       Id = x.Id,
                       Name = x.Name,
                       UserId = x.UserId,
                       Bus = x.Bus != null ?
                               new BusItemModel() { Id = x.Bus.Id, Brand = x.Bus.Brand, LicensePlateNumber = x.Bus.LicensePlateNumber } : null
                   }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
