namespace Terjeki.Scheduler.Application
{
    public class GetCapacitiesQueryHandler : IRequestHandler<GetCapacities, IEnumerable<CapacityModel>>
    {
        private readonly AppDbContext _dbContext;

        public GetCapacitiesQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<CapacityModel>> Handle(GetCapacities request, CancellationToken cancellationToken)
        {
            return await _dbContext.Capacities.Select(x => new CapacityModel()
            {
                Extra = x.Extra,
                Seats = x.Seats,
            }).ToListAsync(cancellationToken);
        }
    }
}
