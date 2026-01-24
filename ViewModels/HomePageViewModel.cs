using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using EShopNative.BaseLibrary;
using EShopNative.Interfaces;
using EShopNative.Pages;
using EShopNative.Services;

namespace EShopNative.ViewModels
{
    public partial class HomePageViewModel : BaseViewModel
    {
        private readonly AuthService _auth;
        private readonly IServiceProvider _services;
        private readonly INavigationService _nav;
        private readonly IAlertService _alert;

        public HomePageViewModel(AuthService auth, 
                                 IServiceProvider services, 
                                 INavigationService nav, 
                                 IAlertService alert)
        {
            _auth = auth;
            _services = services;
            _nav = nav;
            _alert = alert;
        }
        [RelayCommand]
        public async Task Logout()
        {
            await _auth.LogoutAsync();

            var loginPage = _services.GetRequiredService<UserRoleEntry>();
            await _nav.SetRootPage(loginPage);

            await _alert.ShowSuccess("You have been logged out.");
        }
    }
}
