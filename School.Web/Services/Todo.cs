using Newtonsoft.Json;
using School.Web.DTO;
using School.Web.Helpers;
using School.Web.ViewModel;
using System.Text;

namespace School.Web.Services
{
    public class Todo
    {
        private readonly IRestClient _client;
        private readonly IConfiguration configuration;

        public Todo(IRestClient client, IConfiguration configuration)
        {
            _client = client;
            this.configuration = configuration;
            _client.BaseUri = new Uri(this.configuration["ApiUrl"]);
            _client.ContentType = RestClient.ApplicationJson;
        }

        public async Task<TodoVM> GetTodos(string token,int pageIndex, int pageSize)
        {

            _client.ResourcePath = $"/api/v1/todo/get/{pageSize}/{pageIndex}";
            var headers = new Dictionary<string, string>
            {
                {"Authorization", token}
            };
            var response = await _client.GetAsync(headers);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TodoVM>(responseData);
                return result == null ? new TodoVM() : result;
            }
            return new TodoVM();
        }

        public async Task<dynamic> AddTodo(string token,TodoDTO model)
        {
            _client.ResourcePath = $"/api/v1/todo/post";
            var data = new
            {
                name = model.Name,
                description = model.Description
            };
            var serialize = JsonConvert.SerializeObject(data);
            var content = new StringContent(serialize,Encoding.UTF8,RestClient.ApplicationJson);
            var headers = new Dictionary<string, string>
            {
                {"Authorization", token}
            };
            var response = await _client.PostAsync(content, headers);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<dynamic>(responseData);
                return result;
            }
            return null;
        }

    }
}
