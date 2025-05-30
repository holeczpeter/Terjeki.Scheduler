namespace Terjeki.Scheduler.Api.Controllers
{
    [Authorize(Roles = "Admin")]
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
        [HttpPost]
        public async Task<DriverModel> Create([FromBody] CreateDriverCommand request, CancellationToken cancellationToken)
        {
            return await this.mediator.Send(request, cancellationToken);
        }
        [HttpPost]
        public async Task<DriverModel> Update([FromBody] UpdateDriverCommand request, CancellationToken cancellationToken)
        {
            return await this.mediator.Send(request, cancellationToken);
        }
        [HttpDelete]
        public async Task Delete([FromBody] DeleteDriverCommand request, CancellationToken cancellationToken)
        {
            await this.mediator.Send(request, cancellationToken);
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
