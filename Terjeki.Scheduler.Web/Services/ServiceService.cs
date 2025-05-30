namespace Terjeki.Scheduler.Web.Services
{
    public class ServiceService : IServiceService
    {
        private readonly HttpClient _httpClient;

        public ServiceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        
        public async Task<IEnumerable<EventModel>> GetAll(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"api/service/GetAll", cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent) return null;

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<EventModel>>()
                       ?? throw new InvalidOperationException("Response content is null.");
            }
            throw new HttpRequestException($"Error fetching services: {response.StatusCode}");
        }
       
        public async Task<IEnumerable<ServiceModel>> GetAllTypesAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync("api/service/getAllTypes", cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return Array.Empty<ServiceModel>();

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<ServiceModel>>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content was null.");
            }

            throw new HttpRequestException($"Service types fetch failed: {response.StatusCode}");
        }
    }
}
