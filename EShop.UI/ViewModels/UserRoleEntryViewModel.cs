using CommunityToolkit.Mvvm.ComponentModel;
using EShop.API.AuthService;
using EShop.Core.BaseLibrary;
using EShop.Core.Enums;
using System.Windows.Input;

namespace EShop.UI.ViewModels
{
    public partial class UserRoleEntryViewModel : BaseViewModel
    {
        private readonly AuthService _authService;

        [ObservableProperty]
        private AppViewState currentView;

        public ICommand NavigateToRegistrationCommand { get; }
        public ICommand NavigateToLoginCommand { get; }
        public ICommand LoginCommand { get; }



        public UserRoleEntryViewModel()
        {
            _authService = new AuthService();
            CurrentView = AppViewState.Welcome;
            NavigateToRegistrationCommand = new Command(NavigateToRegistration);
            NavigateToLoginCommand = new Command<string>(async (role) => await NavigateToLogin(role));
            LoginCommand = new Command(async () => await UserLogin(Preferences.Get("UserRole", "Guest")));
        }

        private void NavigateToRegistration()
        {
            var role = Preferences.Get("UserRole", "Guest");

            if (role.Equals("Seller", StringComparison.OrdinalIgnoreCase))
                CurrentView = AppViewState.SellerRegistration;
            else
                CurrentView = AppViewState.CustomerRegistration;
        }
        private async Task NavigateToLogin(string role)
        {
            if (role.ToString() == "Customer" || role.ToString() == "Seller")
            {
                bool confirm = await Application.Current.MainPage.
                DisplayAlert("Confirm Role", $"You selected '{role}' as your role. Do you want to proceed?", "Yes", "No");
                if (confirm)
                {
                    Preferences.Set("UserRole", role);
                    CurrentView = AppViewState.Login;
                }
            }
            else if (role.ToString() == "SellerLogin" || role.ToString() == "CustomerLogin")
            {
                CurrentView = AppViewState.Login;
            }

        }
        private async Task UserLogin(string role)
        {
            var authService = new AuthService();
            var profile = await _authService.LoginAsync();

            if (profile != null)
            {
                // Optional: store profile in memory or Preferences
                Preferences.Set("user_id", profile.KeycloakUserId);
                Preferences.Set("display_name", profile.DisplayName);
                Preferences.Set("is_shop_owner", profile.IsShopOwner);

                // Navigate based on role
                //if (profile.IsShopOwner)
                //await Shell.Current.GoToAsync("//ShopDashboard");
                //else
                //await Shell.Current.GoToAsync("//CustomerDashboard");
                // Implement your login logic here based on the role
                await Application.Current.MainPage.DisplayAlert("Login", $"Logging in as {role}", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login Failed", "Unable to authenticate.", "OK");
            }

        }
    }
}
