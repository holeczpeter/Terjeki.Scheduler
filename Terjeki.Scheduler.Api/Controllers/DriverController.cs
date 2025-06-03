namespace Terjeki.Scheduler.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<DriverController> _logger;
        public DriverController(IMediator mediator, ILogger<DriverController> logger)
        {
            this.mediator = mediator;
            this._logger = logger;
        }
        [HttpPost("create")]
        public async Task<DriverModel> Create([FromBody] CreateDriverCommand request, CancellationToken cancellationToken)
        {
            return await this.mediator.Send(request, cancellationToken);
        }
        [HttpPost("update")]
        public async Task<DriverModel> Update([FromBody] UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            return await this.mediator.Send(request, cancellationToken);
        }
        [HttpDelete("{id}")]
        public async Task<bool> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return await this.mediator.Send(new DeleteDriverCommand(id), cancellationToken);
        }
        [HttpGet("Get")]
        public async Task<DriverModel> Get([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetDriverQuery(id);
            return await mediator.Send(query, cancellationToken);
        }

        [HttpGet("GetAll")]
        public async Task<IEnumerable<DriverModel>> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetDriversQuery();
            return await mediator.Send(query, cancellationToken);
        }
    }
}
