namespace Terjeki.Scheduler.Application.Service
{
    internal class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, bool>
    {
        private readonly AppDbContext _dbContext;

        public DeleteServiceCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
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
