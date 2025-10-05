using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EShop.Core.BaseLibrary;
using EShop.Core.Enums;
using System.Windows.Input;

namespace EShop.UI.ViewModels
{
    public partial class UserRoleEntryViewModel : BaseViewModel
    {
        [ObservableProperty]
        private AppViewState currentView;

        public ICommand NavigateToRegistrationCommand { get; }
        public ICommand NavigateToLoginCommand { get; }
        

        public UserRoleEntryViewModel()
        {
            CurrentView = AppViewState.Welcome;
            NavigateToRegistrationCommand = new Command(NavigateToRegistration);
            NavigateToLoginCommand = new Command<string>(async (role) => await NavigateToLogin(role));
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
    }
}
