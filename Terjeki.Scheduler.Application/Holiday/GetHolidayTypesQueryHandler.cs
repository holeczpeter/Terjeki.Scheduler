namespace Terjeki.Scheduler.Application.Holiday
{
    internal class GetHolidayTypesQueryHandler : IRequestHandler<GetHolidayTypesQuery, IEnumerable<HolidayModel>>
    {
        private readonly IMockDatabase _mockDatabase;
        public GetHolidayTypesQueryHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<IEnumerable<HolidayModel>> Handle(GetHolidayTypesQuery request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.GetHolidayTypes(cancellationToken);
        }
    }
}
