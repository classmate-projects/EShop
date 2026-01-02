using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using EShopNative.BaseLibrary;
using EShopNative.Pages;
using EShopNative.ViewModels;
using EShopNative.Models;

namespace EShop
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }
        protected override Window CreateWindow(IActivationState? activationState)
        {
            if (SupabaseConfig.SupabaseClient == null)
            {
                // Initialize asynchronously without blocking
                _ = SupabaseConfig.InitializeAsync(
                    "https://rzaruttlxttwcdrvdzll.supabase.co",
                    "sb_publishable_xN7i9pocdOioMUGuOb3kRw_MKd1JoiC"
                );
            }

            var vm = new UserRoleEntryViewModel(SupabaseConfig.SupabaseClient);
            return new Window(new UserRoleEntry(vm));
        }
        protected override async void OnStart()
        {
            try
            {
                var seeder = new SeedAdminUser();
                await seeder.SeedAdminUserAsync();

                var authRecords = await SupabaseConfig.SupabaseClient
                    .From<AppAuth>()
                    .Select("*")
                    .Get();

                var matched = authRecords.Models.FirstOrDefault(record => BCrypt.Net.BCrypt.Verify("ADMIN_KEY_2026", record.AuthKey) && record.IsActive);
                if (matched != null)
                    await Toast.Make("Configuration verified!", ToastDuration.Short).Show();
                else
                    await Toast.Make("Invalid configuration.", ToastDuration.Short).Show();
            }
            catch (Exception ex)
            {
                await Toast.Make($"Connection failed: {ex.Message}", ToastDuration.Long).Show();
            }
        }
    }
}