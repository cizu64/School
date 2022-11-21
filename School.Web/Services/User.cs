using Newtonsoft.Json;
using School.Web.Helpers;
using System.Net.Http.Headers;

namespace School.Web.Services
{
    public class User
    {
        private readonly HttpClient _client;
        private readonly IConfiguration configuration;

        public User(IConfiguration configuration)
        {
            _client = new HttpClient();
            this.configuration = configuration;
            _client.BaseAddress = new Uri(this.configuration["AuthUrl"]);
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(RestClient.ApplicationJson));
        }

        public async Task<int?> GetUserIdFromToken(string token)
        {
            var resourcePath = $"/api/v1/auth/usid?token={token}";
            var response = await _client.GetAsync(resourcePath);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var usid = JsonConvert.DeserializeObject<int?>(responseData);
                return usid;
            }
            return null;
        }
      
    }
}
