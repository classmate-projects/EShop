using CommunityToolkit.Mvvm.ComponentModel;
using EShopNative.BaseLibrary;
using EShopNative.Enums;
using EShopNative.Pages;
using EShopNative.Services;
using System.Windows.Input;

namespace EShopNative.ViewModels
{
    public partial class UserRoleEntryViewModel : BaseViewModel
    {
        private readonly AuthService _authService = new();
        public string Email { get; set; }
        public string Password { get; set; }


        [ObservableProperty]
        private AppViewState currentView;

        public ICommand NavigateToRegistrationCommand { get; }
        public ICommand NavigateToLoginCommand { get; }
        public ICommand LoginCommand { get; }



        public UserRoleEntryViewModel()
        {
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
            var result = await _authService.LoginAsync(Email, Password);

            if (result.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Login successful", "OK");
                Application.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", result.ErrorMessage, "OK");
            }

        }
    }
}
