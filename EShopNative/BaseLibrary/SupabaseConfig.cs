using Supabase;

namespace EShopNative.BaseLibrary
{
    public static class SupabaseConfig
    {
        public static Client SupabaseClient { get; private set; }

        public static async Task InitializeAsync(string url, string key)
        {
            var options = new SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            SupabaseClient = new Client(url, key, options);
            await SupabaseClient.InitializeAsync();
        }
    }
}
