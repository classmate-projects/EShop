using Android.App;
using Android.Content.PM;
using Android.OS;

namespace EShop
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    [IntentFilter(
    new[] { Android.Content.Intent.ActionView },
    Categories = new[] {
        Android.Content.Intent.CategoryDefault,
        Android.Content.Intent.CategoryBrowsable
    },
    DataScheme = "eshop",
    DataHost = "callback"
)]

    public class MainActivity : MauiAppCompatActivity
    {
    }
}
