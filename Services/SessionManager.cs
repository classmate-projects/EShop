using EShopNative.DataTransferObject;
using EShopNative.Interfaces;
using System.Text.Json;

namespace EShopNative.Services
{
    public class SessionManager : ISessionManager
    {
        public bool IsLoggedIn => !string.IsNullOrEmpty(AccessToken);
        public string? AccessToken { get; private set; }
        public string? RefreshToken { get; private set; }
        public UserDTO? CurrentUser { get; private set; }

        public async Task LoadSessionAsync()
        {
            AccessToken = await SecureStorage.GetAsync("access_token");
            RefreshToken = await SecureStorage.GetAsync("refresh_token");

            var userJson = await SecureStorage.GetAsync("user");
            if (!string.IsNullOrEmpty(userJson))
                CurrentUser = JsonSerializer.Deserialize<UserDTO>(userJson);
        }

        public async Task SaveSessionAsync(string accessToken, string refreshToken, UserDTO user)
        {
            await SecureStorage.SetAsync("access_token", accessToken);
            await SecureStorage.SetAsync("refresh_token", refreshToken);
            await SecureStorage.SetAsync("user", JsonSerializer.Serialize(user));

            AccessToken = accessToken;
            RefreshToken = refreshToken;
            CurrentUser = user;
        }

        public async Task ClearSessionAsync()
        {
            AccessToken = null;
            RefreshToken = null;
            CurrentUser = null;

            SecureStorage.Remove("AccessToken");
            SecureStorage.Remove("RefreshToken");
            SecureStorage.Remove("CurrentUser");

            await Task.CompletedTask;
        }
    }
}
