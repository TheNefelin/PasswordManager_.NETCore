using MauiAppAdmin.ViewModels;

namespace MauiAppAdmin.Views;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfileViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}