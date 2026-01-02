using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using EShopNative.Models;

namespace EShopNative.BaseLibrary
{
    public class SeedAdminUser
    {
        public async Task SeedAdminUserAsync()
        {
            try
            {
                // Fetch all existing auth records
                var existingAuths = await SupabaseConfig.SupabaseClient
                    .From<AppAuth>()
                    .Select("*")
                    .Get();

                // Check if any record matches the raw key
                bool keyAlreadyExists = existingAuths.Models.Any(auth =>
                    BCrypt.Net.BCrypt.Verify("ADMIN_KEY_2026", auth.AuthKey));

                if (keyAlreadyExists)
                {
                    Console.WriteLine("Authentication key already exists. Skipping seed.");
                    return;
                }

                // Insert new hashed auth record
                var newAuth = new AppAuth
                {
                    AuthKey = BCrypt.Net.BCrypt.HashPassword("ADMIN_KEY_2026"),
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                await SupabaseConfig.SupabaseClient
                    .From<AppAuth>()
                    .Insert(newAuth);

                await Toast.Make("Authentication seeded successfully.", ToastDuration.Short).Show();
            }
            catch (Exception ex)
            {
                await Toast.Make("Seeding failed: {ex.Message}", ToastDuration.Short).Show();
            }

        }
    }
}
