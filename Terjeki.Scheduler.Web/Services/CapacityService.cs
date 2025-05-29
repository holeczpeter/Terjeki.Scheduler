using System.Net.Http.Json;

namespace Terjeki.Scheduler.Web.Services
{
    public class CapacityService : ICapacityService
    {
        private readonly HttpClient _httpClient;

        public CapacityService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("ApiClient");
        }
        public async Task<IEnumerable<CapacityModel>> GetAll(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"api/capacity/getAll", cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent) return Enumerable.Empty<CapacityModel>();

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<CapacityModel>>(cancellationToken)
                    ?? throw new InvalidOperationException("Response content is null.");
            }

            throw new HttpRequestException($"Capacity list fetch failed: {response.StatusCode}");
        }
    }
}
