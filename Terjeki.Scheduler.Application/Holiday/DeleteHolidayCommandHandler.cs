namespace Terjeki.Scheduler.Application
{
    public class DeleteHolidayCommandHandler : IRequestHandler<DeleteHolidayCommand, bool>
    {
        private readonly AppDbContext _dbContext;

        public DeleteHolidayCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

     
        public async Task<bool> Handle(DeleteHolidayCommand request, CancellationToken cancellationToken)
        {
            var current = await _dbContext.Events.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            if (current != null)
            {
                current.EntityStatus = EntityStatuses.Deleted;
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;

        }
    }
}
