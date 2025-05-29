namespace Terjeki.Scheduler.Application
{
    public class GetDriversQueryHandler : IRequestHandler<GetDriversQuery, IEnumerable<DriverModel>>
    {
        private readonly AppDbContext _dbContext;
        public GetDriversQueryHandler(AppDbContext dbContext) => _dbContext = dbContext;

        public async Task<IEnumerable<DriverModel>> Handle(GetDriversQuery request, CancellationToken cancellationToken)
        {
            return await this._dbContext.Drivers
                 .Where(x => x.EntityStatus == EntityStatuses.Active)
                 .Select(x => new DriverModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Bus = x.Bus != null ?
                            new BusItemModel() { Id = x.Bus.Id, Brand = x.Bus.Brand, LicensePlateNumber = x.Bus.LicensePlateNumber } : null
                 }).ToListAsync(cancellationToken);
        }
    }
}
