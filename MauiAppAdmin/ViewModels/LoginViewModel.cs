using ClassLibrary.Models.DTOs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppAdmin.Services;
using MauiAppAdmin.Views;
using System.Windows.Input;

namespace MauiAppAdmin.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly IApiAuthService _apiAuthService;
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        private AuthLogin auth = new();

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private bool isErrorVisible;

        [ObservableProperty]
        private bool isLoading = false;

        [ObservableProperty]
        private bool isEnabled = true;

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel(IApiAuthService apiAuthService, IServiceProvider serviceProvider)
        {
            _apiAuthService = apiAuthService;
            _serviceProvider = serviceProvider;
            LoginCommand = new RelayCommand(OnLogin);
            RegisterCommand = new RelayCommand(OnRegister);
        }

        private async void OnLogin()
        {
            if (string.IsNullOrWhiteSpace(Auth.Email) || string.IsNullOrWhiteSpace(Auth.Password))
            {
                ErrorMessage = "Correo y contraseña son obligatorios.";
                IsErrorVisible = true;
                return;
            }

            IsLoading = true;
            IsEnabled = !IsLoading;

            var response = await _apiAuthService.LoginAsync(Auth);

            if (!response.IsSucces)
            {
                ErrorMessage = $"Error: {response.StatusCode}, {response.Message}";
                IsErrorVisible = true;

                IsLoading = false;
                IsEnabled = !IsLoading;

                return;
            }

            await Storage.SaveUser(response.Data);

            var appShell = _serviceProvider.GetRequiredService<AppShell>();
            if (Application.Current?.Windows.Count > 0)
            {
                Application.Current.Windows[0].Page = appShell;
            }

            IsLoading = false;
            IsEnabled = !IsLoading;
        }

        private async void OnRegister()
        {
            var mainPage = Application.Current?.Windows.FirstOrDefault()?.Page;
            if (mainPage != null)
            {
                var registerPage = _serviceProvider.GetRequiredService<RegisterPage>();
                await mainPage.Navigation.PushAsync(registerPage);
            }
        }
    }
}
