using EShopNative.ViewModels;

namespace EShopNative.Pages;

public partial class UserRoleEntry : ContentPage
{
	public UserRoleEntry(UserRoleEntryViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}