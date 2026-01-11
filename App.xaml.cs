using EShopNative;
using EShopNative.Pages;
using EShopNative.ViewModels;

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
            var page = MauiProgram.ServiceProvider.GetService<UserRoleEntry>();
            return new Window(page);
        }

    }
}