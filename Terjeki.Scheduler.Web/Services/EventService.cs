namespace Terjeki.Scheduler.Web.Services
{
    public class EventService : IEventService
    {
        private readonly HttpClient _httpClient;

        public EventService(HttpClient httpClient)
        {
            
            _httpClient = httpClient;
        }

        public async Task<EventModel> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"api/event/get?id={id}", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EventModel>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content is null.");
            }

            throw new HttpRequestException($"Event fetch failed: {response.StatusCode}");
        }

        public async Task<IEnumerable<EventModel>> GetEvents(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"api/event/list", cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent) return Enumerable.Empty<EventModel>();

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<EventModel>>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content is null.");
            }

            throw new HttpRequestException($"Events fetch failed: {response.StatusCode}");
        }

        public async Task<IEnumerable<EventGroupModel>> GetGrouped(DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            var fromStr = from.ToString("s"); 
            var toStr = to.ToString("s");
            var response = await _httpClient.GetAsync(
                $"api/event/group?from={fromStr}&to={toStr}", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<EventGroupModel>>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content is null.");
            }

            throw new HttpRequestException($"Grouped events fetch failed: {response.StatusCode}");
        }

        public async Task<IEnumerable<DriverEventModel>> GetDriverEvents(DateTime from, DateTime to, CancellationToken cancellationToken = default)
        {
            var fromStr = from.ToString("s");
            var toStr = to.ToString("s");
            var response = await _httpClient.GetAsync(
               $"api/event/driver-events?from={fromStr}&to={toStr}", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<DriverEventModel>>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content is null.");
            }

            throw new HttpRequestException($"Driver events fetch failed: {response.StatusCode}");
        }

        public async Task<EventModel> Create(CreateEventCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/event/create", command, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EventModel>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content is null.");
            }

            throw new HttpRequestException($"Event creation failed: {response.StatusCode}");
        }

        public async Task<EventModel> Update(UpdateEventCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/event/update", command, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EventModel>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content is null.");
            }

            throw new HttpRequestException($"Event update failed: {response.StatusCode}");
        }
        public async Task<bool> Undo(UndoLastChangeCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/event/undo", command, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Event deletion failed: {response.StatusCode}");
            }
            return true;
            
        }

        public async Task<bool> Delete(DeleteEventCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"api/event/{command.Id}", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Event deletion failed: {response.StatusCode}");
            }
            return true;
        }
    }
}
