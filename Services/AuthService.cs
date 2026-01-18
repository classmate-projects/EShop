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

        public class RegisterResponse
        {
            public string? Message { get; set; }
            public List<string>? Errors { get; set; }
        }

        public async Task<(bool IsSuccess, string Message, UserDto User)> LoginAsync(LoginRequest request)
        {
            var json = JsonSerializer.Serialize(request);
            var content = new StringContent(json, Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.PostAsync(ApiEndpoints.Login, content);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return (false, responseBody, null);
            }

            var result = JsonSerializer.Deserialize<LoginResponse>(responseBody,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return (true, result?.Message ?? "Login successful", result?.User);
        }
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync(ApiEndpoints.Registration, request);
            var json = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<RegisterResponse>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
            {
                return new RegisterResponse
                {
                    Message = "Registration failed",
                    Errors = result?.Errors ?? new List<string> { "Unknown error occurred" }
                };
            }

            return new RegisterResponse
            {
                Message = result?.Message ?? "Registration successful",
                Errors = null
            };
        }
    }
}
