namespace Terjeki.Scheduler.Web.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("ApiClient");
        }

        /// <summary>
        /// Létrehoz egy új felhasználót.
        /// </summary>
        public async Task<UserModel> CreateAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/Create", command, cancellationToken);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<UserModel>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content was null.");

            throw new HttpRequestException($"User creation failed: {response.StatusCode}");
        }

        /// <summary>
        /// Frissíti a meglévő felhasználót.
        /// </summary>
        public async Task<UserModel> UpdateAsync(UpdateUserCommand command, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/Update", command, cancellationToken);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<UserModel>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content was null.");

            throw new HttpRequestException($"User update failed: {response.StatusCode}");
        }

        /// <summary>
        /// Törli a megadott felhasználót.
        /// </summary>
        public async Task<bool> DeleteAsync(DeleteUserCommand command, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, "api/user")
            {
                Content = JsonContent.Create(command)
            };
            var response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"User delete failed: {response.StatusCode}");
            }
            return true;
        }
        public async Task<IEnumerable<RoleModel>> GetRolesAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync("api/user/GetRoles", cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return Array.Empty<RoleModel>();

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<RoleModel>>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content was null.");
            }

            throw new HttpRequestException($"Fetching roles failed: {response.StatusCode}");
        }


        public async Task<UserModel> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"api/user/Get?id={id}", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserModel>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content was null.");
            }

            throw new HttpRequestException($"Fetching user failed: {response.StatusCode}");
        }


        public async Task<IEnumerable<UserModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync("api/user/GetAll", cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return Array.Empty<UserModel>();

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<UserModel>>(cancellationToken)
                       ?? throw new InvalidOperationException("Response content was null.");
            }

            throw new HttpRequestException($"Fetching users failed: {response.StatusCode}");
        }
    }
}
