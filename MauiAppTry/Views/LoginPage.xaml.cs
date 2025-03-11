using CommunityToolkit.Mvvm.Input;
using MauiAppTry.ViewModel;

namespace MauiAppTry.Views;

public partial class LoginPage : ContentPage
{
    public IRelayCommand LoginCommand { get; }

    public LoginPage()
	{
		InitializeComponent();
        BindingContext = new LoginPageModel();
    }
}