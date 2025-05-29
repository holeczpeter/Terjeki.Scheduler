namespace Terjeki.Scheduler.Application
{
    public class UpdateBusCommandHandler : IRequestHandler<UpdateBusCommand, BusModel>
    {
        private readonly AppDbContext _dbContext;

        public UpdateBusCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BusModel> Handle(UpdateBusCommand request, CancellationToken cancellationToken)
        {
            var current = await this._dbContext.Buses
               .Where(x => x.Id == request.Id && x.EntityStatus == EntityStatuses.Active).FirstOrDefaultAsync(cancellationToken);
            if (current == null) return null;
            var currentCapacity = await this._dbContext.Capacities
                 .Where(x => x.Seats == request.Capacity.Seats).FirstOrDefaultAsync(cancellationToken);
           
            var driver = await _dbContext.Drivers
                .Where(d => d.Id == request.DriverId)
                .FirstOrDefaultAsync(cancellationToken);

            current.Description = request.Description;
            current.LicensePlateNumber = request.LicensePlateNumber;
            current.Brand = request.Name;
            current.Driver = driver;
            current.CurrentMileage = request.CurrentMileage;
            current.Capacity = currentCapacity;

            await this._dbContext.SaveChangesAsync(cancellationToken);

            return await this._dbContext.Buses
               .Where(x => x.Id == current.Id)
               .Select(x => new BusModel
               {

                   Id = x.Id,
                   Capacity = new CapacityModel() { Seats = x.Capacity.Seats, Extra = x.Capacity.Extra },
                   Description = x.Description,
                   LicensePlateNumber = x.LicensePlateNumber,
                   Brand = x.Brand,
                   CurrentMileage = x.CurrentMileage,
                   Driver = x.Driver != null ?
                             new DriverModel() { Id = x.Driver.Id, Name = x.Driver.Name } :
                             null,
               }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
