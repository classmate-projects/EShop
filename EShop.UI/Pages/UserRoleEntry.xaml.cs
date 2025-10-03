using EShop.UI.ViewModels;

namespace EShop.UI.Pages;

public partial class UserRoleEntry : ContentPage
{
	public UserRoleEntry()
	{
		InitializeComponent();
		BindingContext = new UserRoleEntryViewModel();
    }
}