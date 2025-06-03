namespace Terjeki.Scheduler.Web.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly HttpClient _httpClient;

        public HolidayService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

       
        public async Task<EventModel> CreateAsync(CreateHolidayCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/holiday/create", command, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EventModel>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content was null.");
            }
            throw new HttpRequestException($"Holiday create failed: {response.StatusCode}");
        }

        public async Task<EventModel> UpdateAsync(UpdateHolidayCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/holiday/update", command, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EventModel>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content was null.");
            }
            throw new HttpRequestException($"Holiday update failed: {response.StatusCode}");
        }

        public async Task<bool> DeleteAsync(DeleteHolidayCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"api/holiday/{command.Id}", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<bool>(cancellationToken);
            }
            throw new HttpRequestException($"Holiday delete failed: {response.StatusCode}");
        }

        public async Task<BusModel> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"api/holiday/get?id={id}", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BusModel>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content was null.");
            }
            throw new HttpRequestException($"Holiday get failed: {response.StatusCode}");
        }

        public async Task<IEnumerable<BusModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync("api/holiday/getAll", cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return Array.Empty<BusModel>();
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<BusModel>>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content was null.");
            }
            throw new HttpRequestException($"Holiday list fetch failed: {response.StatusCode}");
        }

        public async Task<IEnumerable<HolidayModel>> GetAllTypesAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync("api/holiday/getAllTypes", cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return Array.Empty<HolidayModel>();
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<HolidayModel>>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content was null.");
            }
            throw new HttpRequestException($"Holiday types fetch failed: {response.StatusCode}");
        }
    }
}
