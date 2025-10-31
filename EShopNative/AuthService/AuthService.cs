using EShopNative.MetaData;
using IdentityModel.OidcClient;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace EShopNative.AuthService
{
    public class AuthService
    {
        private readonly OidcClient _oidcClient;

        public AuthService()
        {
            var options = new OidcClientOptions
            {
                Authority = "http://192.168.87.1:8080/realms/EshopRealm",
                ClientId = "eshop-app",
                Scope = "openid profile",
                RedirectUri = "eshop://callback",
                Browser = new WebAuthenticatorBrowser()
            };


            _oidcClient = new OidcClient(options);
        }

        public async Task<AppUserProfile> LoginAsync()
        {
            var result = await _oidcClient.LoginAsync(new LoginRequest());

            if (result.IsError)
                return null;

            var accessToken = result.AccessToken;
            await SecureStorage.SetAsync("access_token", accessToken);

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(accessToken);

            var userId = token.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            var name = token.Claims.FirstOrDefault(c => c.Type == "name")?.Value;

            var roles = token.Claims
                .Where(c => c.Type == "realm_access")
                .SelectMany(c => JsonDocument.Parse(c.Value).RootElement.GetProperty("roles").EnumerateArray())
                .Select(r => r.GetString())
                .ToList();

            var profile = new AppUserProfile
            {
                KeycloakUserId = userId,
                DisplayName = name,
                IsShopOwner = roles.Contains("shop_owner")
            };

            return profile;
        }

    }
}
