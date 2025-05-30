namespace Terjeki.Scheduler.Api.Controllers
{
    [Authorize(Roles = "Admin")]
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
        [HttpPost]
        public async Task<UserModel> Create([FromBody] CreateUserCommand request, CancellationToken cancellationToken)
        {
            return await this._mediator.Send(request, cancellationToken);
        }
        [HttpPost]
        public async Task<UserModel> Update([FromBody] UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return await this._mediator.Send(request, cancellationToken);
        }
        [HttpDelete]
        public async Task<bool> Delete([FromBody] DeleteUserCommand request, CancellationToken cancellationToken)
        {
            return await this._mediator.Send(request, cancellationToken);
        }
        [HttpGet("GetRoles")]
        public async Task<IEnumerable<RoleModel>> GetRoles(CancellationToken cancellationToken)
        {
            var query = new GetRolesQuery();
            return await _mediator.Send(query, cancellationToken);
        }
        [HttpGet("Get")]
        public async Task<UserModel> Get([FromQuery] Guid id,CancellationToken cancellationToken)
        {
            var query = new GetUserQuery(id);
            return await _mediator.Send(query, cancellationToken);
        }
        [HttpGet("GetAll")]
        public async Task<IEnumerable<UserModel>> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetUsersQuery();
            return await _mediator.Send(query, cancellationToken);
        }
    }
}
