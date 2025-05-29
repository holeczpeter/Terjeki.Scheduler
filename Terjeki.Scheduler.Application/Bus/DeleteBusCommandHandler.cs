namespace Terjeki.Scheduler.Application
{
    internal class DeleteBusCommandHandler : IRequestHandler<DeleteBusCommand, bool>
    {
        private readonly AppDbContext _dbContext;
        public DeleteBusCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(DeleteBusCommand request, CancellationToken cancellationToken)
        {
            var current = await this._dbContext.Buses
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
