using System.Windows.Input;

namespace EShopNative.Views;

public partial class CustomNavBar : ContentView
{
    // Title property
    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(CustomNavBar), default(string));

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    // BackCommand property
    public static readonly BindableProperty BackCommandProperty =
        BindableProperty.Create(nameof(BackCommand), typeof(ICommand), typeof(CustomNavBar), null);

    public ICommand BackCommand
    {
        get => (ICommand)GetValue(BackCommandProperty);
        set => SetValue(BackCommandProperty, value);
    }

    // MenuCommand property
    public static readonly BindableProperty MenuCommandProperty =
        BindableProperty.Create(nameof(MenuCommand), typeof(ICommand), typeof(CustomNavBar), null);

    public ICommand MenuCommand
    {
        get => (ICommand)GetValue(MenuCommandProperty);
        set => SetValue(MenuCommandProperty, value);
    }

    public CustomNavBar()
	{
		InitializeComponent(); 
		BindingContext = this;
    }
}