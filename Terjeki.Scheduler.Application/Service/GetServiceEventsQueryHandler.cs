namespace Terjeki.Scheduler.Application
{
    public class GetServiceEventsQueryHandler : IRequestHandler<GetServiceEventsQuery, IEnumerable<EventModel>>
    {
        private readonly AppDbContext _dbContext;

        public GetServiceEventsQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        public async Task<IEnumerable<EventModel>> Handle(GetServiceEventsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Events.Where(e => e.Type == EventTypes.Service && e.EntityStatus == EntityStatuses.Active)
                .Select(e=> new EventModel()
                {
                    Id = e.Id,
                    Capacity = new CapacityModel() { Seats = e.Bus.Capacity.Seats, Extra = e.Bus.Capacity.Extra },
                    Bus = new BusModel()
                    {
                        Id = e.Bus.Id,
                        Capacity = new CapacityModel() { Seats = e.Bus.Capacity.Seats, Extra = e.Bus.Capacity.Extra } ,
                        Description =  e.Bus.Description,
                        LicensePlateNumber =e.Bus.LicensePlateNumber,
                        Brand =  e.Bus.Brand,
                        Driver =  new DriverModel() { Id = e.Bus.Driver.Id, Name = e.Bus.Driver.Name },
                    },
                    Description = e.Description,
                    Drivers = e.DriverEvents.Select(x=> new DriverModel() { Id = x.Driver.Id, Name = x.Driver.Name }).ToList(),
                    EndDate = e.EndDate,
                    StartDate = e.StartDate,
                    Status = e.Status,
                    Type = e.Type,
                    ServiceType = e.ServiceType,
                })
                .ToListAsync(cancellationToken);
        }
    }
}
