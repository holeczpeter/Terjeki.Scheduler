namespace Terjeki.Scheduler.Application
{
    internal class UpdateHolidayCommandHandler : IRequestHandler<UpdateHolidayCommand, EventModel>
    {
        private readonly AppDbContext _dbContext;

        public UpdateHolidayCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

      
        public async Task<EventModel> Handle(UpdateHolidayCommand request, CancellationToken cancellationToken)
        {
            var currentEvent = await _dbContext.Events.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

            
            currentEvent.Description = request.Description;
            currentEvent.EndDate = request.EndDate;
            currentEvent.StartDate = request.StartDate;
            currentEvent.Type = EventTypes.Holiday;
            currentEvent.HolidayType = request.HolidayType;

            await _dbContext.SaveChangesAsync(cancellationToken);

            var incomingDriverIds = new HashSet<Guid> { request.DriverId };
            var existingDriverEvents = _dbContext.DriverEvents.Where(x => x.EventId == request.Id).ToList();


            var driverIdsToRemove = existingDriverEvents
               .Select(de => de.DriverId)
               .Except(incomingDriverIds)
               .ToList();

            foreach (var removeId in driverIdsToRemove)
            {
                var driverEvent = existingDriverEvents.First(de => de.DriverId == removeId);
                driverEvent.EntityStatus = EntityStatuses.Deleted;
            }

            var driverIdsToAdd = incomingDriverIds
                .Except(existingDriverEvents.Select(de => de.DriverId))
                .ToList();

            foreach (var addId in driverIdsToAdd)
            {
                var newDriverEvent = new DriverEvent
                {
                    Id = Guid.NewGuid(),
                    EntityStatus = EntityStatuses.Active,
                    DriverId = addId,
                    EventId = currentEvent.Id
                };
                _dbContext.DriverEvents.Add(newDriverEvent);
            }

            return await _dbContext.Events.Where(x => x.Id == currentEvent.Id).Select(x => new EventModel()
            {
                Id = x.Id,
                Description = x.Description,
                Drivers = x.DriverEvents.Select(d => new DriverItemModel() { Id = d.DriverId, Name = d.Driver.Name }).ToList(),
                EndDate = x.EndDate,
                StartDate = x.StartDate,
                Status = x.Status,
                Type = x.Type,
                ServiceType = x.ServiceType,

            }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
