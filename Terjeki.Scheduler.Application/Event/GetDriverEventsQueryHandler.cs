namespace Terjeki.Scheduler.Application
{
    public class GetDriverEventsQueryHandler : IRequestHandler<GetDriverEventsQueryByInterval, IEnumerable<DriverEventModel>>
    {
        private readonly AppDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        public GetDriverEventsQueryHandler(AppDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }


        public async Task<IEnumerable<DriverEventModel>> Handle(GetDriverEventsQueryByInterval request, CancellationToken cancellationToken)
        {
            var currentUserId = _currentUserService.GetUserId();
            var role = _currentUserService.GetUserRole();
            var isAdmin = string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase);
           
            
            var query = _dbContext.Drivers
               .AsNoTracking()
               .Where(d => d.EntityStatus == EntityStatuses.Active)
               .GroupJoin(
                   _dbContext.DriverEvents
                       .AsNoTracking()
                       .Include(de => de.Event)
                       .Where(de =>
                           de.EntityStatus == EntityStatuses.Active &&
                           de.Event.StartDate.Date <= request.End.Date &&
                           de.Event.EndDate.Date >= request.Start.Date
                       ),
                   driver => driver.Id,
                   de => de.DriverId,
                   (driver, events) => new { driver, events }
               );

            // Nem admin esetén csak a saját drivert mutatjuk
            if (!isAdmin)
            {
                var myDriver = await _dbContext.Drivers
                    .AsNoTracking()
                    .SingleOrDefaultAsync(d => d.UserId == currentUserId, cancellationToken);

                if (myDriver == null)
                    return Array.Empty<DriverEventModel>();

                query = query.Where(x => x.driver.Id == myDriver.Id);
            }

            var result = await query
                .Select(x => new DriverEventModel
                {
                    Driver = new DriverItemModel
                    {
                        Id = x.driver.Id,
                        Name = x.driver.Name
                    },
                    Events = x.events.Select(de => new EventModel
                    {
                        Id = de.Event.Id,
                        StartDate = de.Event.StartDate,
                        EndDate = de.Event.EndDate,
                        Summary = de.Event.Summary,
                        Description = de.Event.Description,
                        Type = de.Event.Type,
                        Status = de.Event.Status,
                        Capacity = de.Event.Type != EventTypes.Holiday ? new CapacityModel
                        {
                            Extra = de.Event.Bus.Capacity.Extra,
                            Seats = de.Event.Bus.Capacity.Seats,

                        } : new CapacityModel(),
                        Bus = de.Event.Type != EventTypes.Holiday ? new BusItemModel
                        {
                            Id = de.Event.Bus.Id,
                            CurrentMileage = de.Event.Bus.CurrentMileage,
                            LicensePlateNumber = de.Event.Bus.LicensePlateNumber,
                            Brand = de.Event.Bus.Brand,
                        } : new BusItemModel(),
                        Drivers = new List<DriverItemModel>() { new DriverItemModel() { Id = de.Driver.Id, Name = de.Driver.Name } } ,
                        ServiceType = de.Event.ServiceType,
                        HolidayType = de.Event.HolidayType
                    }).ToList()
                })
                .ToListAsync(cancellationToken);

            return result;
        }

    }
}
