using ReturnOrderService.Data.Interfaces;
using System.Text;
using System.Text.Json;

namespace ReturnOrderService.Data
{
    public class HttpClientDataContext : IHttpClientDataContext
    {
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _clientConfiguration;
        private readonly HttpClient _client;

        public HttpClientDataContext(IConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            _client = client;
            _clientConfiguration = _configuration.GetSection("DummyHttpClient");
        }

        public async Task<HttpResponseMessage> GksOrderReturnAsync(string source, dynamic request)
        {
            var endpoint = _configuration.GetSection(source)["OrderReturn"];
            string payload = JsonSerializer.Serialize(request);
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri(_configuration.GetSection(source)["BaseAddress"] + endpoint),
                Content = new StringContent(payload, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Post,
            };
            return await _client.SendAsync(httpRequestMessage);
        }
    }
}
