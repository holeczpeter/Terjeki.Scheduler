using Terjeki.Scheduler.Core.Model.Email;

namespace Terjeki.Scheduler.Application
{
    internal class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, EventModel>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMediator _mediator;
        public CreateServiceCommandHandler(AppDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }
        public async Task<EventModel> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
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


           
            if (request.IsNotification)
            {
                var drivers = await _dbContext.Buses
                         .Select(d => d.Driver.Name)
                         .ToListAsync(cancellationToken);
                foreach (var driver in drivers)
                {
                   
                    var htmlBody = Messages.BuildServiceDowntimeMessage(driver, currentBus, request.StartDate,request.EndDate, request.ServiceType.GetDescription());
                    var message = new MessageModel
                    {
                        To = "holecz.peter85@gmail.com",
                        Subject = "Új szervíz",
                        Body = htmlBody.ToString()
                    };
                    await _mediator.Publish(message);
                }

            }
            if (currentBus != null) { currentBus.CurrentMileage = request.CurrentMileage; };
            await _dbContext.SaveChangesAsync(cancellationToken);
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
