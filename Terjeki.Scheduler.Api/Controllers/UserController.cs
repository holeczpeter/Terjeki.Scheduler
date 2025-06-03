namespace Terjeki.Scheduler.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;
        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

       

        [HttpGet("GetAllDrivers")]
        public async Task<IEnumerable<UserModel>> GetAllDrivers(CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(new GetAllDriversQuery(), cancellationToken);
        }
        
    }
}
