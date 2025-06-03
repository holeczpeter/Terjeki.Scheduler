namespace Terjeki.Scheduler.Application
{
    internal class UpdateDriverCommandHandler : IRequestHandler<UpdateDriverCommand, DriverModel>
    {
        private readonly AppDbContext _dbContext;

        public UpdateDriverCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<DriverModel> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            var currentBus = await this._dbContext.Buses
              .Where(x => x.Id == request.BusId).FirstOrDefaultAsync(cancellationToken);
            var currentDriver = await this._dbContext.Drivers.Where(x=>x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            var currentUser = await _dbContext.Users.Where(x => x.Id == request.UserId).FirstOrDefaultAsync(cancellationToken);
            currentDriver.Bus = currentBus;
            currentDriver.Name = request.UserId != null ? currentUser.FullName : request.DriverName;
            currentDriver.UserId = request.UserId;  

            await this._dbContext.SaveChangesAsync(cancellationToken);

            return await this._dbContext.Drivers
                .Where(x => x.Id == request.Id)
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

