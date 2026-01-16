using EShopNative.BaseLibrary;
using EShopNative.DataTransferObject;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;


namespace EShopNative.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(AppConstants.BaseApiUrl)
            };
        }

        public async Task<(bool IsSuccess, string Message, UserDto User)> LoginAsync(LoginRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync("/eshop/user/login", content);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return (false, responseBody, null);
            }

            var result = JsonSerializer.Deserialize<LoginResponse>(responseBody,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return (true, result?.Message ?? "Login successful", result?.User);
        }
        public async Task<string> RegisterAsync(RegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register", request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"Error: {error}";
            }

            return "Registration successful";
        }
    }
}
