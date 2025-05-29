namespace Terjeki.Scheduler.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class CapacityController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<CapacityController> _logger;
        public CapacityController(IMediator mediator, ILogger<CapacityController> logger)
        {
            this.mediator = mediator;
            this._logger = logger;
        }
        [HttpGet("GetAll")]
        public async Task<IEnumerable<CapacityModel>> GetCapacities(CancellationToken cancellationToken)
        {
            var query = new GetCapacities();
            return await this.mediator.Send(query, cancellationToken);
        }
    }
}
