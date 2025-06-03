namespace Terjeki.Scheduler.Application
{
    internal class DeleteAllowedEmailCommandHandler : IRequestHandler<DeleteAllowedEmailCommand, bool>
    {
       
        private readonly AppDbContext _dbContext;

        public DeleteAllowedEmailCommandHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<bool> Handle(DeleteAllowedEmailCommand request, CancellationToken cancellationToken)
        {
            var current = await _dbContext.AllowedEmails.Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
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
