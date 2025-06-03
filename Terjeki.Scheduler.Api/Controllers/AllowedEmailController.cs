namespace Terjeki.Scheduler.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AllowedEmailController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AllowedEmailController> _logger;

        public AllowedEmailController(IMediator mediator, ILogger<AllowedEmailController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpPost]
        public async Task<AllowedEmailModel> Create([FromBody] CreateAllowedEmailCommand request, CancellationToken cancellationToken)
        {
            return await this._mediator.Send(request, cancellationToken);
        }
        [HttpPost]
        public async Task<AllowedEmailModel> Update([FromBody] UpdateAllowedEmailCommand request, CancellationToken cancellationToken)
        {
            return await this._mediator.Send(request, cancellationToken);
        }
        [HttpDelete("{id}")]
        public async Task<bool> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return await this._mediator.Send(new DeleteAllowedEmailCommand(id), cancellationToken);
        }
        [HttpGet("GetRoles")]
        public async Task<IEnumerable<RoleModel>> GetRoles(CancellationToken cancellationToken)
        {
            var query = new GetRolesQuery();
            return await _mediator.Send(query, cancellationToken);
        }
        [HttpGet("Get")]
        public async Task<AllowedEmailModel> Get([FromQuery] Guid id,CancellationToken cancellationToken)
        {
            var query = new GetAllowedEmailQuery(id);
            return await _mediator.Send(query, cancellationToken);
        }
        [HttpGet("GetAll")]
        public async Task<IEnumerable<AllowedEmailModel>> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllowedEmailsQuery();
            return await _mediator.Send(query, cancellationToken);
        }
    }
}
