namespace Terjeki.Scheduler.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class HolidayController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<HolidayController> _logger;

        public HolidayController(IMediator mediator, ILogger<HolidayController> logger)
        {
            this.mediator = mediator;
            this._logger = logger;
        }

        [HttpPost("create")]
        public async Task<EventModel> Create([FromBody] CreateHolidayCommand request, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }

        [HttpPost("update")]
        public async Task<EventModel> Update([FromBody] UpdateHolidayCommand request, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return await mediator.Send(new DeleteHolidayCommand(id), cancellationToken);
        }

        [HttpGet("get")]
        public async Task<EventModel> Get([FromQuery] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetEventQuery(id);
            return await mediator.Send(query, cancellationToken);
        }

        [HttpGet("getAll")]
        public async Task<IEnumerable<BusModel>> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetBusesQuery();
            return await mediator.Send(query, cancellationToken);
        }
        [HttpGet("getAllTypes")]
        public async Task<IEnumerable<HolidayModel>> GetAllTypes(CancellationToken cancellationToken)
        {
            var query = new GetHolidayTypesQuery();
            return await mediator.Send(query, cancellationToken);
        }
    }
}
