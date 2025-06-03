using System.Net.Http.Json;

namespace Terjeki.Scheduler.Web.Services
{
    public class BusService : IBusService
    {
        private readonly HttpClient _httpClient;

        public BusService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

     
        public async Task<BusModel> Get(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"api/bus/get?id={id}", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BusModel>(cancellationToken)
                    ?? throw new InvalidOperationException("Response content is null.");
            }

            throw new HttpRequestException($"Bus fetch failed: {response.StatusCode}");
        }

        public async Task<IEnumerable<BusModel>> GetAll(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"api/bus/getAll", cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent) return Enumerable.Empty<BusModel>();

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<BusModel>>(cancellationToken)
                    ?? throw new InvalidOperationException("Response content is null.");
            }

            throw new HttpRequestException($"Bus list fetch failed: {response.StatusCode}");
        }

        public async Task<BusModel> Create(CreateBusCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/bus/create", command, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BusModel>(cancellationToken)
                    ?? throw new InvalidOperationException("Response content is null.");
            }

            throw new HttpRequestException($"Bus creation failed: {response.StatusCode}");
        }

        public async Task<BusModel> Update(UpdateBusCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/bus/update", command, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BusModel>(cancellationToken)
                    ?? throw new InvalidOperationException("Response content is null.");
            }

            throw new HttpRequestException($"Bus update failed: {response.StatusCode}");
        }

        public async Task<bool> Delete(DeleteBusCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"api/bus/{command.Id}", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Bus deletion failed: {response.StatusCode}");
            }
            return true;
        }
    }
}
