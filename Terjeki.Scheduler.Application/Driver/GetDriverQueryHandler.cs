namespace Terjeki.Scheduler.Application
{
    public class GetDriverQueryHandler : IRequestHandler<GetDriverQuery, DriverModel>
    {
        private readonly AppDbContext _dbContext;

        public GetDriverQueryHandler(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        public async Task<DriverModel> Handle(GetDriverQuery request, CancellationToken cancellationToken)
        {
            return await this._dbContext.Drivers
                 .Where(x => x.Id == request.Id)
                 .Select(x => new DriverModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     UserId = x.UserId,
                     Bus = x.Bus != null ?
                            new BusItemModel() { Id = x.Bus.Id, Brand = x.Bus.Brand, LicensePlateNumber = x.Bus.LicensePlateNumber } : null
                 }).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
