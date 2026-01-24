using EShopNative.ViewModels;

namespace EShopNative.Pages;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}