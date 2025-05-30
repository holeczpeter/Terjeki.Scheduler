namespace Terjeki.Scheduler.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class BusController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<BusController> _logger;

        public BusController(IMediator mediator, ILogger<BusController> logger)
        {
            this.mediator = mediator;
            this._logger = logger;
        }

        [HttpPost("create")]
        public async Task<BusModel> Create([FromBody] CreateBusCommand request, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }

        [HttpPost("update")]
        public async Task<BusModel> Update([FromBody] UpdateBusCommand request, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }

        [HttpDelete]
        public async Task Delete([FromBody] DeleteBusCommand request, CancellationToken cancellationToken)
        {
            await mediator.Send(request, cancellationToken);
        }

        [HttpGet("get")]
        public async Task<BusModel> Get([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetBusQuery(id);
            return await mediator.Send(query, cancellationToken);
        }

        [HttpGet("getAll")]
        public async Task<IEnumerable<BusModel>> GetBuses(CancellationToken cancellationToken)
        {
            var query = new GetBusesQuery();
            return await mediator.Send(query, cancellationToken);
        }
    }
}
