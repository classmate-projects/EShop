using System.Net.Http.Json;

namespace EShopNative.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService()
        {
            var baseUrl = DeviceInfo.Platform == DevicePlatform.Android
                ? "http://10.0.2.2:5250"   // Android emulator loopback
                : "http://localhost:5250"; // Windows/macOS

            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> LoginAsync(string email, string password)
        {
            var loginData = new { Email = email, Password = password };
            var response = await _httpClient.PostAsJsonAsync("/api/user/login", loginData);

            if (response.IsSuccessStatusCode)
                return (true, null);

            var error = await response.Content.ReadAsStringAsync();
            return (false, error);
        }
    }
}
