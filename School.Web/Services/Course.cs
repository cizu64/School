using Newtonsoft.Json;
using School.Web.Helpers;
using School.Web.ViewModel;

namespace School.Web.Services
{
    public class Course
    {
        private readonly IRestClient _client;
        private readonly IConfiguration configuration;

        public Course(IRestClient client, IConfiguration configuration)
        {
            _client = client;
            this.configuration = configuration;
            _client.BaseUri = new Uri(this.configuration["ApiUrl"]);
            _client.ContentType = RestClient.ApplicationJson;
        }

        public async Task<CourseVM> GetCourses(string token,int pageIndex, int pageSize)
        {
            _client.ResourcePath = $"/api/v1/student/courses/{pageSize}/{pageIndex}";
            var headers = new Dictionary<string, string>
            {
                {"Authorization", token}
            };
            var response = await _client.GetAsync(headers);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<CourseVM>(responseData);
                return result == null ? new CourseVM() : result;
            }
            return new CourseVM();
        }

       
    }
}
