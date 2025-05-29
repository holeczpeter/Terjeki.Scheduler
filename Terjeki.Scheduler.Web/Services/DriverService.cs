using System.Net.Http.Json;

namespace Terjeki.Scheduler.Web.Services
{
    public class DriverService : IDriverService
    {
        private readonly HttpClient _httpClient;
        public DriverService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("ApiClient");

        }
        public async Task<DriverModel> Create(CreateDriverCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/driver/create", command, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<DriverModel>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content is null.");
            }

            throw new HttpRequestException($"Driver create failed: {response.StatusCode}");
        }
        public async Task<DriverModel> Update(UpdateDriverCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/driver/update", command, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<DriverModel>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content is null.");
            }

            throw new HttpRequestException($"Driver update failed: {response.StatusCode}");
        }
        public async Task<bool> Delete(DeleteDriverCommand command, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "api/driver")
            {
                Content = JsonContent.Create(command)
            };

            var response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Driver delete failed: {response.StatusCode}");
            }
            return true;
        }
        public async Task<DriverModel> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"api/driver/Get?id={id}", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<DriverModel>()
                       ?? throw new InvalidOperationException("Response content is null.");
            }
            throw new HttpRequestException($"Error fetching driver: {response.StatusCode}");
        }

        public async Task<IEnumerable<DriverModel>> GetAll(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"api/driver/GetAll", cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent) return null;

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<DriverModel>>()
                       ?? throw new InvalidOperationException("Response content is null.");
            }
            throw new HttpRequestException($"Error fetching drivers: {response.StatusCode}");
        }

    }
}

