using MauiAppAdmin.ViewModels;

namespace MauiAppAdmin.Views;

public partial class CorePage : ContentPage
{
	public CorePage(CoreViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}