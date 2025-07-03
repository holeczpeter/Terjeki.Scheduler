using Terjeki.Scheduler.Core.Model.Email;

namespace Terjeki.Scheduler.Application
{
    internal class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, EventModel>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMediator _mediator;
        public UpdateServiceCommandHandler(AppDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }


        public async Task<EventModel> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
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

            if (currentEvent.Type == EventTypes.Service)
            {
                if (currentBus != null) { currentBus.CurrentMileage = request.CurrentMileage; };
            }
            if (request.IsNotification)
            {
                var drivers = await _dbContext.Buses
                         .Where(x=>x.Id == currentBus.Id)
                         .Select(d => d.Driver.Name)
                         .ToListAsync(cancellationToken);
                foreach (var driver in drivers)
                {

                    var htmlBody = Messages.BuildModifedServiceDowntimeMessage(driver, currentBus, request.StartDate, request.EndDate, request.ServiceType.GetDescription());
                    var message = new MessageModel
                    {
                        To = "holecz.peter85@gmail.com",
                        Subject = "Szervíz esemény módosult",
                        Body = htmlBody.ToString()
                    };
                    await _mediator.Publish(message);
                }

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
