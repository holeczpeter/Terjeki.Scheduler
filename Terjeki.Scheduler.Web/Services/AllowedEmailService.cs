namespace Terjeki.Scheduler.Web.Services
{
    public class AllowedEmailService : IAllowedEmailService
    {
        private readonly HttpClient _httpClient;

        public AllowedEmailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        

        /// <summary>
        /// Létrehoz egy új felhasználót.
        /// </summary>
        public async Task<AllowedEmailModel> CreateAsync(CreateAllowedEmailCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/allowedEmail/Create", command, cancellationToken);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<AllowedEmailModel>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content was null.");

            throw new HttpRequestException($"Allowed email creation failed: {response.StatusCode}");
        }

        /// <summary>
        /// Frissíti a meglévő felhasználót.
        /// </summary>
        public async Task<AllowedEmailModel> UpdateAsync(UpdateAllowedEmailCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/allowedEmail/Update", command, cancellationToken);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<AllowedEmailModel>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content was null.");

            throw new HttpRequestException($"Allowed email update failed: {response.StatusCode}");
        }

        /// <summary>
        /// Törli a megadott felhasználót.
        /// </summary>
        public async Task<bool> DeleteAsync(DeleteAllowedEmailCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.DeleteAsync($"api/allowedEmail/{command.Id}", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Allowed email delete failed: {response.StatusCode}");
            }
            return true;
        }
        public async Task<IEnumerable<RoleModel>> GetRolesAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync("api/allowedEmail/GetRoles", cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return Array.Empty<RoleModel>();

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<RoleModel>>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content was null.");
            }

            throw new HttpRequestException($"Fetching roles failed: {response.StatusCode}");
        }


        public async Task<AllowedEmailModel> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"api/allowedEmail/Get?id={id}", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<AllowedEmailModel>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content was null.");
            }

            throw new HttpRequestException($"Fetching allowed email failed: {response.StatusCode}");
        }


        public async Task<IEnumerable<AllowedEmailModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync("api/allowedEmail/GetAll", cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return Array.Empty<AllowedEmailModel>();

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<AllowedEmailModel>>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content was null.");
            }

            throw new HttpRequestException($"Fetching allowed emails failed: {response.StatusCode}");
        }
    }
}
