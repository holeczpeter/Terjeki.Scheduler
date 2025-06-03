namespace Terjeki.Scheduler.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(IMediator mediator, ILogger<ServiceController> logger)
        {
            this.mediator = mediator;
            this._logger = logger;
        }

        [HttpPost("create")]
        public async Task<EventModel> Create([FromBody] CreateServiceCommand request, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }
        [HttpPost("update")]
        public async Task<EventModel> Update([FromBody] UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            await mediator.Send(new DeleteServiceCommand(id), cancellationToken);
        }
       
        [HttpGet("getAll")]
        public async Task<IEnumerable<EventModel>> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetServiceEventsQuery();
            return await mediator.Send(query, cancellationToken);
        }
        [HttpGet("getAllTypes")]
        public async Task<IEnumerable<ServiceModel>> GetAllTypes(CancellationToken cancellationToken)
        {
            var query = new GetServiceTypesQuery();
            return await mediator.Send(query, cancellationToken);
        }
    }
}
