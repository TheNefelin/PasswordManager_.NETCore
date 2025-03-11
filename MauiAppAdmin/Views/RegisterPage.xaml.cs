using MauiAppAdmin.ViewModels;

namespace MauiAppAdmin.Views;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(RegisterPageModel viewModel)
    {
		InitializeComponent();
        BindingContext = viewModel;
    }
}