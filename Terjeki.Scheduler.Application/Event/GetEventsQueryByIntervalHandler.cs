namespace Terjeki.Scheduler.Application
{
    public class GetEventsQueryByIntervalHandler : IRequestHandler<GetEventsQueryByInterval, IEnumerable<EventModel>>
    {
        private readonly AppDbContext _dbContext;

        public GetEventsQueryByIntervalHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

     
        public async Task<IEnumerable<EventModel>> Handle(GetEventsQueryByInterval request, CancellationToken cancellationToken)
        {

            return await _dbContext.Events
                .Where(e => e.StartDate.Date <= request.End.Date && 
                            request.Start.Date <= e.EndDate.Date && 
                            e.EntityStatus == EntityStatuses.Active)
                .Select(x => new EventModel()
                {
                    Id = x.Id,
                    Capacity = new CapacityModel() { Seats = x.Bus.Capacity.Seats, Extra = x.Bus.Capacity.Extra },
                    Bus = new BusItemModel
                    {
                        Id = x.Bus.Id,
                        LicensePlateNumber = x.Bus.LicensePlateNumber,
                        Brand = x.Bus.Brand,
                       
                    },
                    Summary = x.Summary,
                    Description = x.Description,
                    Drivers = x.DriverEvents.Select(d => new DriverItemModel() { Id = d.DriverId, Name = d.Driver.Name }).ToList(),
                    EndDate = x.EndDate,
                    StartDate = x.StartDate,
                    Status = x.Status,
                    Type = x.Type,
                    ServiceType = x.ServiceType,

                }).ToListAsync(cancellationToken);
        }
    }
}
