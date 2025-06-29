using System.Text;

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
            var htmlBody = new StringBuilder();
            htmlBody.AppendLine("<!DOCTYPE html>");
            htmlBody.AppendLine("<html lang=\"hu\">");
            htmlBody.AppendLine("<head><meta charset=\"UTF-8\"><style>");
            htmlBody.AppendLine(" body { font-family: Arial, sans-serif; color: #333; }");
            htmlBody.AppendLine(" .container { max-width: 600px; margin: auto; padding: 20px; }");
            htmlBody.AppendLine(" h2 { color: #005CAF; }");
            htmlBody.AppendLine(" table { width: 100%; border-collapse: collapse; margin-top: 20px; }");
            htmlBody.AppendLine(" th, td { padding: 10px; border-bottom: 1px solid #e0e0e0; text-align: left; }");
            htmlBody.AppendLine(" th { background-color: #f5f5f5; }");
            htmlBody.AppendLine(" .footer { margin-top: 30px; font-size: 0.9em; color: #777; }");
            htmlBody.AppendLine("</style></head>");
            htmlBody.AppendLine("<body><div class=\"container\">");
            htmlBody.AppendLine("  <h2>Utazás részletei</h2>");
            htmlBody.AppendLine($"  <p>Kedves <strong>{newEvent.DriverEvents.Select(x=>x.Driver.Name.FirstOrDefault())}</strong>,</p>");
            htmlBody.AppendLine("  <p>Az alábbiakban láthatod az utazásod adatait:</p>");
            htmlBody.AppendLine("  <table>");
            htmlBody.AppendLine($"    <tr><th>Címzett</th><td>{newEvent.DriverEvents.Select(x => x.Driver.Name.FirstOrDefault())}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Indulás</th><td>{request.StartDate:yyyy-MM-dd HH:mm}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Érkezés</th><td>{request.EndDate:yyyy-MM-dd HH:mm}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Leírás</th><td>{request.Description}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Részletek</th><td>{request.Summary}</td></tr>");
            htmlBody.AppendLine($"    <tr><th>Busz rendszáma</th><td>{currentBus.LicensePlateNumber}</td></tr>");
            htmlBody.AppendLine("  </table>");
            htmlBody.AppendLine("  <p class=\"footer\">");
            htmlBody.AppendLine("    Ha bármilyen kérdésed van, kérlek, jelezd vissza.<br>");
            htmlBody.AppendLine("    Jó utat kívánunk!<br><em>A Te Csapatod</em>");
            htmlBody.AppendLine("  </p>");
            htmlBody.AppendLine("</div></body></html>");

            // --- üzenet összerakása és kiküldése via MediatR+EmailMessageHandler ---
            var message = new MessageModel
            {
                To = "holecz.peter85@gmail.com",
                Subject = "Új esemény – utazás részletei",
                Body = htmlBody.ToString()
            };
  

            await _mediator.Publish(message);

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
