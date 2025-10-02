using EShop.UI.ViewModels;

namespace EShop.UI.Pages;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
		BindingContext = new WelcomePageViewModel();
    }
}