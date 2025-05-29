namespace Terjeki.Scheduler.Application
{
    internal class GetHolidayTypesQueryHandler : IRequestHandler<GetHolidayTypesQuery, IEnumerable<HolidayModel>>
    {
       
        public GetHolidayTypesQueryHandler()
        {
          
        }
        public async Task<IEnumerable<HolidayModel>> Handle(GetHolidayTypesQuery request, CancellationToken cancellationToken)
        {
            return Enum.GetValues(typeof(HolidayTypes))
                .Cast<HolidayTypes>()
                .Where(x => x != HolidayTypes.None)
                .Select(service => new HolidayModel
                {

                    Name = service.GetDescription(),
                    Type = service
                })
                .ToList();
        }
    }
}
