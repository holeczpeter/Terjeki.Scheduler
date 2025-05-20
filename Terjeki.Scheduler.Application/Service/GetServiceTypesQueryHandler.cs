namespace Terjeki.Scheduler.Application.Service
{

    internal class GetServiceTypesQueryHandler : IRequestHandler<GetServiceTypesQuery, IEnumerable<ServiceModel>>
    {
        private readonly IMockDatabase _mockDatabase;
        public GetServiceTypesQueryHandler(IMockDatabase mockDatabase)
        {
            _mockDatabase = mockDatabase;
        }
        public async Task<IEnumerable<ServiceModel>> Handle(GetServiceTypesQuery request, CancellationToken cancellationToken)
        {
            return await _mockDatabase.GetServiceTypes(cancellationToken);
        }
    }
}
