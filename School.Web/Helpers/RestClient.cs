using System.Net.Http.Headers;

namespace School.Web.Helpers
{

    public interface IRestClient
    {
        /// <summary>
        /// The base uri of the resource
        /// </summary>
        Uri BaseUri { get; set; }

        /// <summary>
        /// The request uri of the resource
        /// </summary>
        string ResourcePath { get; set; }

        /// <summary>
        /// The resource content type
        /// </summary>
        string ContentType { get; set; }

        /// <summary>
        /// Send a Get request. Pass header information in the parameter if any
        /// </summary>
        /// <returns></returns>
        Task<HttpResponseMessage> GetAsync(IEnumerable<KeyValuePair<string, string>> headers = null);

        /// <summary>
        /// Send a post request
        /// </summary>
        /// <param name="content"></param>
        /// <param name="headers">Pass header informatiuon if any</param>
        /// <returns></returns>
        Task<HttpResponseMessage> PostAsync(HttpContent content,
            IEnumerable<KeyValuePair<string, string>> headers = null);

        /// <summary>
        /// Send a put request
        /// </summary>
        /// <param name="content"></param>
        /// <param name="headers">Pass header informatiuon if any</param>
        /// <returns></returns>
        Task<HttpResponseMessage> PutAsync(HttpContent content, IEnumerable<KeyValuePair<string, string>> headers = null);

        /// <summary>
        /// Delete a resource
        /// </summary>
        /// <param name="content"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> DeleteAsync(
            IEnumerable<KeyValuePair<string, string>> headers = null);


    }

    /// <summary>
    /// A class for consuming web api from a.net client
    /// </summary>
    public class RestClient : IRestClient
    {
        /// <summary>
        /// The base uri of the resource
        /// </summary>
        public Uri BaseUri { get; set; }

        /// <summary>
        /// The request uri of the resource
        /// </summary>
        public string ResourcePath { get; set; }

        /// <summary>
        /// The resource content type
        /// </summary>
        public string ContentType { get; set; }

        public static string ApplicationXml = "application/xml";
        public static string TextXml = "text/xml";
        public static string ApplicationJson = "application/json";
        public static string TextJson = "text/json";
        private readonly IHttpClientFactory _client;
        public RestClient(IHttpClientFactory client)
        {
            _client = client;
        }

        /// <summary>
        /// Configure the client to consume the Api
        /// </summary>
        /// <returns></returns>
        private HttpClient ConfigureClient(IEnumerable<KeyValuePair<string, string>> headers = null)
        {
            var httpClient = _client.CreateClient();
            httpClient.BaseAddress = new Uri(BaseUri.AbsoluteUri);

            if (headers != null)
                foreach (var items in headers)
                {
                    httpClient.DefaultRequestHeaders.Add(items.Key, items.Value);
                }
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ContentType));
            return httpClient;
        }

        /// <summary>
        /// Send a Get request. Pass header information in the parameter if any
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAsync(IEnumerable<KeyValuePair<string, string>> headers = null)
        {
            var client = ConfigureClient(headers);
            var response = await client.GetAsync(ResourcePath);
            return response;
        }

        /// <summary>
        /// Send a Get request. Pass header information in the parameter if any
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get(IEnumerable<KeyValuePair<string, string>> headers = null)
        {
            var client = ConfigureClient(headers);
            var response = client.GetAsync(ResourcePath);
            return response.Result;
        }

        /// <summary>
        /// Send a post request
        /// </summary>
        /// <param name="content"></param>
        /// <param name="headers">Pass header information if any</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PostAsync(HttpContent content, IEnumerable<KeyValuePair<string, string>> headers = null)
        {
            var client = ConfigureClient(headers);
            var response = await client.PostAsync(ResourcePath, content);
            return response;
        }

        /// <summary>
        /// Send a put request
        /// </summary>
        /// <param name="content"></param>
        /// <param name="headers">Pass header informatiuon if any</param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> PutAsync(HttpContent content, IEnumerable<KeyValuePair<string, string>> headers = null)
        {
            var client = ConfigureClient(headers);
            var response = await client.PutAsync(ResourcePath, content);
            return response;
        }
        /// <summary>
        /// Delete a resource
        /// </summary>
        /// <param name="content"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> DeleteAsync(IEnumerable<KeyValuePair<string, string>> headers = null)
        {
            var client = ConfigureClient(headers);
            var response = await client.DeleteAsync(ResourcePath);
            return response;
        }


    }
}