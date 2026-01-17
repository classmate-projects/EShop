using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using EShopNative.BaseLibrary;
using EShopNative.DataTransferObject;
using EShopNative.Enums;
using EShopNative.Pages;
using EShopNative.Services;
using System.Text.RegularExpressions;
using System.Windows.Input;
using static EShopNative.Services.AuthService;

namespace EShopNative.ViewModels
{
    public partial class UserRoleEntryViewModel : BaseViewModel
    {
        private readonly AuthService _authService;

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        private AppViewState _currentView;
        public AppViewState CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged(); // must notify UI
                }
            }
        }

        public ICommand NavigateToRegistrationCommand { get; }
        public ICommand NavigateToLoginCommand { get; }
        public ICommand LoginCommand { get; }
        public ICommand RegistrationCommand { get; }


        public UserRoleEntryViewModel(AuthService authService)
        {
            _authService = authService;


            CurrentView = AppViewState.Welcome;
            NavigateToRegistrationCommand = new Command(NavigateToRegistration);
            NavigateToLoginCommand = new Command<string>(async (role) => await NavigateToLogin(role));
            LoginCommand = new Command(async () => await UserLogin(Email, Password));
            RegistrationCommand = new Command(async () => await UserSignUP(Name, Email, Password, ConfirmPassword));
        }

        private void NavigateToRegistration()
        {
            var role = Preferences.Get("UserRole", "Guest");

            if (role.Equals("Seller"))
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
        private async Task UserLogin(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email and password are required", "OK");
                return;
            }

            var request = new LoginRequest
            {
                Username = email,
                Password = password
            };

            var (isSuccess, message, user) = await _authService.LoginAsync(request);

            if (!isSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Login Failed", message, "OK");
                return;
            }
            else
            {
                // Success
                await Application.Current.MainPage.DisplayAlert("Success", "Login successful", "OK");

                // Navigate to next page
                await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
            }   
        }


        public async Task UserSignUP(string name, string email, string password, string confirmPassword)
        {
            try
            {
                // Step 0: Check if all fields are filled
                if (string.IsNullOrWhiteSpace(name) ||
                    string.IsNullOrWhiteSpace(email) ||
                    string.IsNullOrWhiteSpace(password) ||
                    string.IsNullOrWhiteSpace(confirmPassword))
                {
                    await Toast.Make("Please fill in all fields.", ToastDuration.Short).Show();
                    return;
                }

                // Step 1b: Validate password strength
                if (password.Length < 6)
                {
                    await Toast.Make("Password must be at least 6 characters.", ToastDuration.Short).Show();
                    return;
                }

                // Step 1c: Validate email format
                var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(email, emailPattern))
                {
                    await Toast.Make("Invalid email format.", ToastDuration.Short).Show();
                    return;
                }

                // Step 2: Confirm password
                if (password != confirmPassword)
                {
                    await Toast.Make("Passwords do not match.", ToastDuration.Short).Show();
                    return;
                }

                var request = new RegisterRequest
                {
                    Username = email,
                    Email = email,
                    Password = password,
                    FullName = name,
                };

                var response = await _authService.RegisterAsync(request);

                // NEW: Handle backend validation errors
                if (response.Errors != null && response.Errors.Any())
                {
                    string errorMessage = string.Join("\n", response.Errors);
                    await Toast.Make(errorMessage, ToastDuration.Long).Show();
                    return;
                }

                // NEW: Handle success message
                if (response.Message == "Registration successful")
                {
                    await Toast.Make("Registration successful!", ToastDuration.Short).Show();
                    await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                }

                // Fallback
                await Toast.Make("Unexpected response from server.", ToastDuration.Short).Show();
            }
            catch (Exception ex)
            {
                await Toast.Make($"Registration failed: {ex.Message}", ToastDuration.Long).Show();
            }
        }
    }
}
