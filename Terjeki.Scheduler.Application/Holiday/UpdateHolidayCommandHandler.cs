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
            var current = await _dbContext.Events.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

            current.Description = request.Description;
            current.EndDate = request.EndDate;
            current.StartDate = request.StartDate;
            current.Type = EventTypes.Holiday;
            current.HolidayType = request.HolidayType;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return await _dbContext.Events.Where(x => x.Id == current.Id).Select(x => new EventModel()
            {

            }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
