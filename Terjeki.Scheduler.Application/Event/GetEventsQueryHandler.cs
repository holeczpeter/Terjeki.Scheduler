namespace Terjeki.Scheduler.Application
{
    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, IEnumerable<EventModel>>
    {
        private readonly AppDbContext _dbContext;

        public GetEventsQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        public async Task<IEnumerable<EventModel>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Events.Where(e => e.EntityStatus == EntityStatuses.Active).Select(x => new EventModel()
            {
                Id = x.Id,
                Bus = x.Type == EventTypes.Holiday ? new BusItemModel() : new BusItemModel
                {
                    Id = x.Bus.Id,
                    CurrentMileage = x.Bus.CurrentMileage,
                    LicensePlateNumber = x.Bus.LicensePlateNumber,
                    Brand = x.Bus.Brand,
                },
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
