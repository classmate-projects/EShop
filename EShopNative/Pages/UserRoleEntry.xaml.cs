using EShopNative.ViewModels;

namespace EShopNative.Pages;

public partial class UserRoleEntry : ContentPage
{
	public UserRoleEntry()
	{
		InitializeComponent();
        BindingContext = new UserRoleEntryViewModel();
    }
}