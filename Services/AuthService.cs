using EShopNative.BaseLibrary;
using EShopNative.DataTransferObject;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


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

            var response = await _httpClient.PostAsync("/api/user/login", content);

            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return (false, responseBody, null);
            }

            var result = JsonSerializer.Deserialize<LoginResponse>(responseBody,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return (true, result?.Message ?? "Login successful", result?.User);
        }
    }
}
