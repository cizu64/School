using Newtonsoft.Json;
using School.Web.Helpers;
using School.Web.ViewModel;
using System.Text;

namespace School.Web.Services
{
    public class Student
    {
        private readonly IRestClient _client;
        private readonly IConfiguration configuration;

        public Student(IRestClient client, IConfiguration configuration)
        {
            _client = client;
            this.configuration = configuration;
            _client.BaseUri = new Uri(this.configuration["ApiUrl"]);
            _client.ContentType = RestClient.ApplicationJson;
        }

        public async Task<StudentVM> GetStudent(string token)
        {
            _client.ResourcePath = $"/api/v1/student/get/";
            var headers = new Dictionary<string, string>
            {
                {"Authorization", token}
            };
            var response = await _client.GetAsync(headers);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<StudentVM>(responseData);
                return result == null ? new StudentVM() : result;
            }
            return new StudentVM();
        }
       
        public async Task<string> Login(string email, string password)
        {
            _client.BaseUri = new Uri(this.configuration["AuthUrl"]); ; //Auth url
            _client.ResourcePath = $"/api/v1/auth/login";
            var data = new
            {
                Email = email,
                Password = password
            };
            var serialize = JsonConvert.SerializeObject(data);
            var content = new StringContent(serialize);
            var response = await _client.PostAsync(content);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(responseData);
                return (string)result.result;//token
            }
            return null;
        }
    }
}
