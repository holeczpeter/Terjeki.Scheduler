using System.Text;
using Terjeki.Scheduler.Core.Model.Email;

namespace Terjeki.Scheduler.Application
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventModel>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMediator _mediator;
        public CreateEventCommandHandler(AppDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }
        public async Task<EventModel> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var currentBus = await _dbContext.Buses.Where(x => x.Id == request.BusId).FirstOrDefaultAsync(cancellationToken);

            var newEvent = new Event()
            {
                Id = Guid.NewGuid(),
                Bus = currentBus,
                Summary = request.Summary,
                Description = request.Description,
                EndDate = request.EndDate,
                StartDate = request.StartDate,
                Status = request.Status,
                Type = request.Type,
                ServiceType = request.ServiceType,

            };
            _dbContext.Events.AddAsync(newEvent, cancellationToken);

            var incomingDrivers = request.DriverIds.ToHashSet();
            foreach (var driver in incomingDrivers)
            {
                var currentDriverEvent = new DriverEvent()
                {
                    DriverId = driver,
                    Event = newEvent
                };
                _dbContext.DriverEvents.AddAsync(currentDriverEvent, cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
            if (request.IsNotification)
            {
                foreach (var driver in incomingDrivers) 
                {
                    var currentDriver = await _dbContext.Drivers
                         .Where(d => d.Id == driver)
                         .Select(d => d.Name)
                         .FirstOrDefaultAsync(cancellationToken);
                    var htmlBody = Messages.BuildNewEventMessage(currentDriver,request,currentBus);
                    var message = new MessageModel
                    {
                        To = "holecz.peter85@gmail.com",
                        Subject = "Új esemény – utazás részletei",
                        Body = htmlBody.ToString()
                    };
                    await _mediator.Publish(message);
                }
            }

            return await _dbContext.Events.Where(x => x.Id == newEvent.Id).Select(x => new EventModel()
            {
                Id = x.Id,
                Capacity = new CapacityModel() { Seats = x.Bus.Capacity.Seats, Extra = x.Bus.Capacity.Extra },
                Bus = new BusItemModel
                {
                    Id = x.Bus.Id,
                    CurrentMileage = x.Bus.CurrentMileage,
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
