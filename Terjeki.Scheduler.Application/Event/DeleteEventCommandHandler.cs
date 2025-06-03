namespace Terjeki.Scheduler.Application
{
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, bool>
    {
        private readonly AppDbContext _dbContext;

        public DeleteEventCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var current = await this._dbContext.Events
              .Where(x => x.Id == request.Id)
              .FirstOrDefaultAsync(cancellationToken);

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
