using CommunityToolkit.Maui;
using EShop;
using EShopNative.BaseLibrary;
using EShopNative.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System.Reflection;

namespace EShopNative
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // Core MAUI setup
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcon");
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Load secrets from UserSecrets
            builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true);
            var url = "https://rzaruttlxttwcdrvdzll.supabase.co";
            var key = "sb_publishable_xN7i9pocdOioMUGuOb3kRw_MKd1JoiC";

            Console.WriteLine($"Supabase URL: {url}");
            Console.WriteLine($"Supabase Key: {key}");


            // Initialize Supabase
            _ = SupabaseConfig.InitializeAsync(url, key);


            // Dependency Injection
            builder.Services.AddSingleton(SupabaseConfig.SupabaseClient);
            builder.Services.AddTransient<UserRoleEntryViewModel>();

            // Custom UI Handlers
            ConfigureCustomHandlers();

#if DEBUG
            builder.Logging.AddDebug();
            builder.Logging.AddConsole();
#endif

            return builder.Build();
        }

        private static void ConfigureCustomHandlers()
        {
            EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.Background = null;
#endif
#if IOS || MACCATALYST
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
#if WINDOWS
                handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
#endif
            });

            PickerHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.Background = null;
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
#endif
#if IOS || MACCATALYST
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                handler.PlatformView.Layer.BorderWidth = 0;
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
#if WINDOWS
                handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
#endif
            });
        }
    }
}