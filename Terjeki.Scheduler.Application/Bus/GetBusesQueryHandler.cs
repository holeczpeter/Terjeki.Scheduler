namespace Terjeki.Scheduler.Application
{
    public class GetBusesQueryHandler : IRequestHandler<GetBusesQuery, IEnumerable<BusModel>>
    {
        private readonly AppDbContext _dbContext;

        public GetBusesQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BusModel>> Handle(GetBusesQuery request, CancellationToken cancellationToken)
        {
            return await this._dbContext.Buses
                  .Where(x => x.EntityStatus == EntityStatuses.Active)
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
               }).ToListAsync(cancellationToken);
            
        }
    }
}
