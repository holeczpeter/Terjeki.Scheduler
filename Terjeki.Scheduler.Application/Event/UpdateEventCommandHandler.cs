namespace Terjeki.Scheduler.Application
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, EventModel>
    {
        private readonly AppDbContext _dbContext;

        public UpdateEventCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        public async Task<EventModel> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {

            var currentBus = await _dbContext.Buses.Where(x => x.Id == request.BusId).FirstOrDefaultAsync(cancellationToken);
            var currentEvent = await _dbContext.Events.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            

            currentEvent.Bus = currentBus;
            currentEvent.Description = request.Description;
            currentEvent.EndDate = request.EndDate;
            currentEvent.StartDate = request.StartDate;
            currentEvent.Status = request.Status;
            currentEvent.Type = request.Type;
            currentEvent.ServiceType = request.ServiceType;
            

            var incomingDriverIds = request.DriverIds.ToHashSet();
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
            await _dbContext.SaveChangesAsync(cancellationToken);
            return await _dbContext.Events.Where(x => x.Id == currentEvent.Id).Select(x => new EventModel()
            {
                Id = x.Id,
                Capacity = new CapacityModel() { Seats = x.Bus.Capacity.Seats, Extra = x.Bus.Capacity.Extra },
                Bus = new BusItemModel
                {
                    Id = x.Bus.Id,
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

            }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
