namespace Terjeki.Scheduler.Application
{
    public class GetBusQueryHandler : IRequestHandler<GetBusQuery, BusModel>
    {
        private readonly AppDbContext _dbContext;

        public GetBusQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<BusModel> Handle(GetBusQuery request, CancellationToken cancellationToken)
        {
            return await this._dbContext.Buses
               .Where(x => x.Id == request.Id)
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
