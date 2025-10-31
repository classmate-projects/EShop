using IdentityModel.OidcClient.Browser;

namespace EShopNative.AuthService
{
    public class WebAuthenticatorBrowser : IdentityModel.OidcClient.Browser.IBrowser
    {
        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            try
            {
                var authResult = await WebAuthenticator.AuthenticateAsync(
                    new Uri(options.StartUrl),
                    new Uri(options.EndUrl));

                var responseUrl = new UriBuilder(options.EndUrl);
                var query = System.Web.HttpUtility.ParseQueryString(string.Empty);

                foreach (var kvp in authResult.Properties)
                    query[kvp.Key] = kvp.Value;

                responseUrl.Query = query.ToString();

                return new BrowserResult
                {
                    Response = responseUrl.ToString(),
                    ResultType = BrowserResultType.Success
                };
            }
            catch (Exception ex)
            {
                return new BrowserResult
                {
                    ResultType = BrowserResultType.UnknownError,
                    Error = ex.Message
                };
            }
        }
    }
}
