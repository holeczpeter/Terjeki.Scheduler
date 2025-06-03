namespace Terjeki.Scheduler.Application
{
    public class GetEventGroupQueryByIntervalHandler : IRequestHandler<GetEventGroupQueryByInterval, IEnumerable<EventGroupModel>>
    {
        private readonly AppDbContext _dbContext;

        public GetEventGroupQueryByIntervalHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<EventGroupModel>> Handle(GetEventGroupQueryByInterval request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Buses
             .Include(b => b.Capacity)
             .GroupJoin(
                 _dbContext.Events
                     .Where(e =>
                         e.EntityStatus == EntityStatuses.Active &&
                         e.BusId != null &&
                         e.StartDate.Date <= request.End.Date &&
                         request.Start.Date <= e.EndDate.Date)
                     .Include(e => e.DriverEvents).ThenInclude(de => de.Driver),
                         bus => bus.Id,
                         ev => ev.BusId!.Value,
                         (bus, events) => new EventGroupModel
                         {
                             Bus = new BusItemModel
                             {
                                 Id = bus.Id,
                                 Brand = bus.Brand,
                                 LicensePlateNumber = bus.LicensePlateNumber,
                     
                             },
                             Capacity = new CapacityModel
                             {
                                 Seats = bus.Capacity.Seats,
                                 Extra = bus.Capacity.Extra
                             },
                             Events = events.Select(ev => new EventModel
                             {
                                 Id = ev.Id,
                                 StartDate = ev.StartDate,
                                 EndDate = ev.EndDate,
                                 Description = ev.Description,
                                 Bus = new BusItemModel
                                 {
                                     Id = ev.Bus.Id,
                                     Brand = ev.Bus.Brand,
                                     LicensePlateNumber = ev.Bus.LicensePlateNumber,
                                 },
                                 Capacity = new CapacityModel
                                 {
                                     Seats = ev.Bus.Capacity.Seats,
                                     Extra = ev.Bus.Capacity.Extra
                                 },
                                 Drivers = ev.DriverEvents
                                     .Where(de => de.EntityStatus == EntityStatuses.Active)
                                     .Select(de => new DriverItemModel
                                     {
                                         Id = de.Driver.Id,
                                         Name = de.Driver.Name
                                     })
                                     .ToList(),
                                 Type = ev.Type,
                                 Status = ev.Status,
                                 ServiceType = ev.ServiceType
                             }).ToList()
             })
            .AsNoTracking()
            .ToListAsync(cancellationToken);

            return result;
        }
    }
}
