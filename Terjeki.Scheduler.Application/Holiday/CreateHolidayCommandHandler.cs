namespace Terjeki.Scheduler.Application
{
    public class CreateHolidayCommandHandler : IRequestHandler<CreateHolidayCommand, EventModel>
    {
        private readonly AppDbContext _dbContext;

        public CreateHolidayCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<EventModel> Handle(CreateHolidayCommand request, CancellationToken cancellationToken)
        {
            var driver = await _dbContext.Drivers
               .Where(d => d.Id == request.Driver.Id)
               .FirstOrDefaultAsync(cancellationToken);
            var newEvent = new Event()
            {
                Id = Guid.NewGuid(),
                Description = request.Description,
                EndDate = request.EndDate,
                StartDate = request.StartDate,
                Type = EventTypes.Holiday,
                HolidayType = request.HolidayType,
            };

            var newRelation = new DriverEvent()
            {
                Driver = driver,
                Event = newEvent,
            };
            await _dbContext.AddAsync(newEvent, cancellationToken);
            await _dbContext.AddAsync(newRelation, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return await _dbContext.Events.Where(x=>x.Id == newEvent.Id).Select(x=> new EventModel() 
            { 
            
            }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
