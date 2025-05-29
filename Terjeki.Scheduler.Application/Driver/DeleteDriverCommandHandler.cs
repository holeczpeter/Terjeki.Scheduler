namespace Terjeki.Scheduler.Application
{
    public class DeleteDriverCommandHandler : IRequestHandler<DeleteDriverCommand, bool>
    {
        private readonly AppDbContext _dbContext;

        public DeleteDriverCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
        {
            var currentDriver = await this._dbContext.Drivers.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);

            if (currentDriver != null)
            {
                currentDriver.EntityStatus = EntityStatuses.Deleted;
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            return false;
            
        }
    }
}
