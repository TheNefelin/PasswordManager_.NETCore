using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppTry.Views;

namespace MauiAppTry.ViewModel
{
    public partial class LoginPageModel : ObservableObject
    {
        public IRelayCommand LoginCommand { get; }
        public IRelayCommand RegisterCommand { get; }

        public LoginPageModel()
        {
            LoginCommand = new RelayCommand(OnLoginButtonClicked);
            RegisterCommand = new RelayCommand(OnRegisterButtonClicked);
        }

        private async void OnLoginButtonClicked()
        {
            if (Application.Current?.Windows?.Count > 0)
            {
                Application.Current.Windows[0].Page = new AppShell();
            }
        }

        private async void OnRegisterButtonClicked()
        {
            if (Application.Current?.Windows?.Count > 0)
            {
                var navigation = Application.Current.Windows[0].Page?.Navigation;
                if (navigation != null)
                {
                    await navigation.PushAsync(new RegisterPage());
                }
            }
        }
    }
}
