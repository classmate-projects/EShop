using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
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
        private readonly ISessionManager _session;

        [ObservableProperty]
        private string userName;

        public HomePageViewModel(AuthService auth, 
                                 IServiceProvider services, 
                                 INavigationService nav, 
                                 IAlertService alert,
                                 ISessionManager session)
        {
            _auth = auth;
            _services = services;
            _session = session;
            _nav = nav;
            _alert = alert;

            userName = session.CurrentUser?.Email ?? "User";
        }

        [RelayCommand]
        public async Task Logout()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                await _session.ClearSessionAsync();

                var loginPage = _services.GetRequiredService<UserRoleEntry>();
                await _nav.SetRootPage(loginPage);

                await _alert.ShowSuccess("Logged out successfully");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
