using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiAppTry.ViewModel
{
    public partial class RegisterPageModel : ObservableObject
    {
        public IRelayCommand LoginCommand { get; }

        public RegisterPageModel()
        {
            LoginCommand = new RelayCommand(OnLoginButtonClicked);
        }

        private async void OnLoginButtonClicked()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
