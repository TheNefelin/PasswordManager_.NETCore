using MauiAppTry.ViewModel;

namespace MauiAppTry.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
        BindingContext = new RegisterPageModel();
    }
}