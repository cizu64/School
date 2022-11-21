using Newtonsoft.Json;
using School.API.Helpers;

namespace School.API.Service
{
    public class User
    {
        private readonly IRestClient _client;
        private readonly IConfiguration configuration;

        public User(IRestClient restclient, IConfiguration configuration)
        {
            _client = restclient;
            this.configuration = configuration;
            _client.BaseUri = new Uri(this.configuration["AuthUrl"]);
            _client.ContentType = RestClient.ApplicationJson;
        }
        public async Task<int?> GetUserIdFromToken(string token)
        {
            _client.ResourcePath = $"/api/v1/auth/usid?token={token}";
            var response = await _client.GetAsync();
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
