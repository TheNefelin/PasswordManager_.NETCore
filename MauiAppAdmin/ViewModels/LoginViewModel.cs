using ClassLibrary.Models.DTOs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppAdmin.Services;
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

        public LoginViewModel(IApiAuthService apiAuthService, IServiceProvider serviceProvider)
        {
            _apiAuthService = apiAuthService;
            _serviceProvider = serviceProvider;
            LoginCommand = new RelayCommand(OnLogin);
        }

        public ICommand LoginCommand { get; }

        private async void OnLogin()
        {
            if (string.IsNullOrWhiteSpace(Auth.Email) || string.IsNullOrWhiteSpace(Auth.Password))
            {
                ErrorMessage = "Correo y contraseña son obligatorios.";
                IsErrorVisible = true;
                return;
            }

            IsLoading = true;
            var response = await _apiAuthService.LoginAsync(Auth);

            if (!response.IsSucces)
            {
                //await Shell.Current.DisplayAlert($"Error {response.StatusCode}", response.Message, "OK");
                ErrorMessage = $"Error: { response.StatusCode }, { response.Message }";
                IsErrorVisible = true;
                IsLoading = false;

                return;
            }

            await Storage.SaveUser(response.Data);
            
            var currentWindow = Application.Current!.Windows.FirstOrDefault();
            if (currentWindow != null)
            {
                Application.Current!.CloseWindow(currentWindow);
            }

            var loginPage = _serviceProvider.GetRequiredService<AppShell>();
            Application.Current!.OpenWindow(new Window(loginPage));

            IsLoading = false;
        }
    }

}
