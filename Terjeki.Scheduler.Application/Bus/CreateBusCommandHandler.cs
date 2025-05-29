namespace Terjeki.Scheduler.Application
{
    public class CreateBusCommandHandler : IRequestHandler<CreateBusCommand, BusModel>
    {
        private readonly AppDbContext _dbContext;
        public CreateBusCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<BusModel> Handle(CreateBusCommand request, CancellationToken cancellationToken)
        {
            var currentCapacity = await this._dbContext.Capacities
                .Where(x => x.Seats == request.Capacity.Seats).FirstOrDefaultAsync(cancellationToken);

            var driver = await _dbContext.Drivers
                .Where(d => d.Id == request.DriverId)
                .FirstOrDefaultAsync(cancellationToken);

            var bus = new Bus()
            {
                Id = Guid.NewGuid(),
                Capacity = currentCapacity,
                Description = request.Description,
                LicensePlateNumber = request.LicensePlateNumber,
                Brand = request.Name,
                Driver = driver,
                CurrentMileage = request.CurrentMileage,
            };
            await this._dbContext.Buses.AddAsync(bus, cancellationToken);
            await this._dbContext.SaveChangesAsync(cancellationToken);

            

            return await this._dbContext.Buses
                .Where(x=>x.Id == bus.Id)
                .Select(x => new BusModel 
                {

                    Id = x.Id,
                    Capacity = new CapacityModel() { Seats = x.Capacity.Seats, Extra = x.Capacity.Extra },
                    Description = x.Description,
                    LicensePlateNumber = x.LicensePlateNumber,
                    Brand = x.Brand,
                    CurrentMileage = x.CurrentMileage,
                    Driver = x.Driver != null ?
                             new DriverModel() { Id = x.Driver.Id, Name = x.Driver.Name }:
                             null,
                }).FirstOrDefaultAsync(cancellationToken);

        }
    }
}
