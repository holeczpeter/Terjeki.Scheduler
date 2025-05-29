namespace Terjeki.Scheduler.Application
{

    internal class GetServiceTypesQueryHandler : IRequestHandler<GetServiceTypesQuery, IEnumerable<ServiceModel>>
    {
       
        public GetServiceTypesQueryHandler()
        {
            
        }
        public async Task<IEnumerable<ServiceModel>> Handle(GetServiceTypesQuery request, CancellationToken cancellationToken)
        {
            return Enum.GetValues(typeof(ServiceTypes))
                .Cast<ServiceTypes>()
                .Where(x => x != ServiceTypes.None)
                .Select(service => new ServiceModel
                {
                    Name = service.GetDescription(),   
                    Type = service               
                })
                .ToList();
        }
    }
}
