using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace FrontEnd.Services
{

    public class ApiClientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7043/api";
        private readonly IHttpContextAccessor _httpContextAccessor;


        public ApiClientService(IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpContextAccessor = httpContextAccessor;
        }

        private void AddJwtToken()
        {
            // Limpiar header antes
            _httpClient.DefaultRequestHeaders.Authorization = null;

            var token = _httpContextAccessor.HttpContext?.Session.GetString("token");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }


        public async Task<T?> GetAsync<T>(string endpoint, bool useToken = false)
        {
            if (useToken)
                AddJwtToken();

            var response = await _httpClient.GetAsync($"{_baseUrl}/{endpoint}");
            if (!response.IsSuccessStatusCode) return default;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<T?> GetListAsync<T>(string endpoint, Dictionary<string, string>? queryParams = null, bool useToken = false)
        {
            if (useToken)
                AddJwtToken();

            var url = $"{_baseUrl}/{endpoint}";

            if (queryParams != null && queryParams.Any())
            {
                var query = string.Join("&", queryParams.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
                url += "?" + query;
            }

            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();

            // Intentar deserializar aunque sea 400
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }




        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data, bool useToken = false)
        {
            if (useToken)
                AddJwtToken();

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/{endpoint}", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponse>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }


    }
}
