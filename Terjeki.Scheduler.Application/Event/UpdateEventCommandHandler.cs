using System.Text;
using Terjeki.Scheduler.Core.Model.Email;

namespace Terjeki.Scheduler.Application
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, EventModel>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMediator _mediator;
        public UpdateEventCommandHandler(AppDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }


        public async Task<EventModel> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {

            var currentBus = await _dbContext.Buses.Where(x => x.Id == request.BusId).FirstOrDefaultAsync(cancellationToken);
            var currentEvent = await _dbContext.Events.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            

            currentEvent.Bus = currentBus;
            currentEvent.Summary = request.Summary;
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
            if (request.IsNotification)
            {
                foreach (var item in driverIdsToAdd)
                {
                    var currentDriver = await _dbContext.Drivers
                                      .Where(d => d.Id == item)
                                      .Select(d => d.Name)
                                      .FirstOrDefaultAsync(cancellationToken);
                    var htmlBody = Messages.BuildNewEventMessage(currentDriver, request, currentBus);
                    var message = new MessageModel
                    {
                        To = "holecz.peter85@gmail.com",
                        Subject = "Új esemény – utazás részletei",
                        Body = htmlBody.ToString()
                    };


                    await _mediator.Publish(message);
                }
                var driverIdsToModify = existingDriverEvents
                  .Select(de => de.DriverId)
                  .Intersect(incomingDriverIds)
                  .ToList();
                foreach (var item in driverIdsToModify)
                {
                    var currentDriver = await _dbContext.Drivers.Where(d => d.Id == item)
                                      .Select(d => d.Name)
                                      .FirstOrDefaultAsync(cancellationToken);
                    var htmlBody = Messages.BuildNewEventMessage(currentDriver, request, currentBus);
                    var message = new MessageModel
                    {
                        To = "holecz.peter85@gmail.com",
                        Subject = "Módosított esemény – utazás részletei",
                        Body = htmlBody.ToString()
                    };


                    await _mediator.Publish(message);
                }
            }
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
                Summary = x.Summary,
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
