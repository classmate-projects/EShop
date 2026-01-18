using CommunityToolkit.Mvvm.ComponentModel;

namespace EShopNative.BaseLibrary
{
    public class BaseViewModel : ObservableObject
    {
    }
    public class AppConstants
    {
        public const string BaseApiUrl = "https://eshopapi-a6haa8b6azg4bqc7.centralindia-01.azurewebsites.net";
    }
    public class ApiEndpoints
    {
        public const string Login = "eshop/user/login";
        public const string Registration = "eshop/user/register";
    }
}
