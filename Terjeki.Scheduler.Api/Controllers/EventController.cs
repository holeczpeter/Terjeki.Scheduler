namespace Terjeki.Scheduler.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<EventController> _logger;

        public EventController(IMediator mediator, ILogger<EventController> logger)
        {
            this.mediator = mediator;
            this._logger = logger;
        }

        [HttpPost("create")]
        public async Task<EventModel> Create([FromBody] CreateEventCommand request, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }

        [HttpPost("update")]
        public async Task<EventModel> Update([FromBody] UpdateEventCommand request, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }

        [HttpDelete]
        public async Task Delete([FromBody] DeleteEventCommand request, CancellationToken cancellationToken)
        {
            await mediator.Send(request, cancellationToken);
        }

        [HttpGet("get")]
        public async Task<EventModel> Get([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetEventQuery(id);
            return await mediator.Send(query, cancellationToken);
        }

        [HttpGet("list")]
        public async Task<IEnumerable<EventModel>> GetEvents(CancellationToken cancellationToken)
        {
            var query = new GetEventsQuery();
            return await mediator.Send(query, cancellationToken);
        }

        [HttpGet("group")]
        public async Task<IEnumerable<EventGroupModel>> GetEventGroupQuery(
            [FromQuery] DateTime from,
            [FromQuery] DateTime to,
            CancellationToken cancellationToken)
        {
            var query = new GetEventGroupQueryByInterval(from,to);
            return await mediator.Send(query, cancellationToken);
        }

        [HttpGet("driver-events")]
        public async Task<IEnumerable<DriverEventModel>> GetDriverEvents(
            [FromQuery] Guid driverId,
            [FromQuery] DateTime from,
            [FromQuery] DateTime to,
            CancellationToken cancellationToken)
        {
            var query = new GetDriverEventsQueryByInterval(to, from);
            
            return await mediator.Send(query, cancellationToken);
        }
    }
}
