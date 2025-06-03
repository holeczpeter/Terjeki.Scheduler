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
                    Bus = new BusItemModel()
                    {
                        Id = e.Bus.Id,
                        LicensePlateNumber =e.Bus.LicensePlateNumber,
                        Brand =  e.Bus.Brand,
                        CurrentMileage = e.Bus.CurrentMileage
                    },
                    Description = e.Description,
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
