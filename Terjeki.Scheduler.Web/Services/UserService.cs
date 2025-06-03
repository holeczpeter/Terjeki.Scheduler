namespace Terjeki.Scheduler.Web.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<UserModel>> GetAllDrivers(CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.GetAsync($"api/user/GetAllDrivers", cancellationToken);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent) return null;

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<UserModel>>()
                       ?? throw new InvalidOperationException("Response content is null.");
            }
            throw new HttpRequestException($"Error fetching users: {response.StatusCode}");
        }

       
    }
}
