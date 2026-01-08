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
            // Create your ViewModel without Supabase dependency
            var vm = new UserRoleEntryViewModel();

            // Pass it into your page
            return new Window(new UserRoleEntry(vm));
        }

    }
}