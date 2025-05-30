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
