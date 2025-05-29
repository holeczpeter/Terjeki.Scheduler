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
               .Where(x => x.Id == request.BusId).FirstOrDefaultAsync(cancellationToken);

            var driver = new Driver()
            {
                Id = Guid.NewGuid(),
                Name = request.Driver.Name,
                Bus = currentBus,
            };
            await this._dbContext.Drivers.AddAsync(driver, cancellationToken);
            await this._dbContext.SaveChangesAsync(cancellationToken);

            return await this._dbContext.Drivers
                .Where(x => x.Id == driver.Id)
                .Select(x => new DriverModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Bus = x.Bus != null ? 
                            new BusItemModel() { Id = x.Bus.Id, Brand = x.Bus.Brand, LicensePlateNumber = x.Bus.LicensePlateNumber } : null
                }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
