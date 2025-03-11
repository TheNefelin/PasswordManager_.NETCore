using ClassLibrary.Models.DTOs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppAdmin.Services;
using System.Windows.Input;

namespace MauiAppAdmin.ViewModels
{
    public partial class RegisterPageModel : ObservableObject
    {
        private readonly IApiAuthService _apiAuthService;

        [ObservableProperty]
        private bool _isLoading = false;

        [ObservableProperty]
        private bool _isEnabled = true;

        [ObservableProperty]
        private bool _isErrorVisible;

        [ObservableProperty]
        private string _errorMessage;

        [ObservableProperty]
        private AuthRegister _authRegister = new();

        public ICommand RegisterCommand { get; }

        public RegisterPageModel(IApiAuthService apiAuthService)
        {
            _apiAuthService = apiAuthService;
            RegisterCommand = new RelayCommand(OnRegister);
        }

        private async void OnRegister()
        {
            if (string.IsNullOrWhiteSpace(AuthRegister.Email) || string.IsNullOrWhiteSpace(AuthRegister.Password1) || string.IsNullOrWhiteSpace(AuthRegister.Password2))
            {
                ErrorMessage = "Correo y contraseña son obligatorios.";
                IsErrorVisible = true;
                return;
            }

            Loading();
            var currentWindow = Application.Current?.Windows.FirstOrDefault();
            var responseApi = await _apiAuthService.RegisterAsync(AuthRegister);

            if (!responseApi.IsSucces)
            {
                ErrorMessage = $"Error: {responseApi.StatusCode}, {responseApi.Message}";
                IsErrorVisible = true;

                NotLoading();
                return;
            }

            await currentWindow.Page.DisplayAlert($"Code: {responseApi.StatusCode}", responseApi.Message, "OK");

            if (currentWindow.Page.Navigation != null)
            {
                await currentWindow.Page.Navigation.PopAsync();
            }

            NotLoading();
        }

        private void Loading() { IsLoading = true; IsEnabled = !IsLoading; }
        private void NotLoading() { IsLoading = false; IsEnabled = !IsLoading; }
    }
}
