namespace Terjeki.Scheduler.Application
{
    public class GetEventQueryHandler : IRequestHandler<GetEventQuery, EventModel>
    {
        private readonly AppDbContext _dbContext;

        public GetEventQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        public async Task<EventModel> Handle(GetEventQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Events.Where(x => x.Id == request.Id).Select(x => new EventModel()
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
