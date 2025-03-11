using ClassLibrary.Models;
using ClassLibrary.Models.DTOs;
using ClassLibrary.Models.Utils;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppAdmin.Components;
using MauiAppAdmin.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiAppAdmin.ViewModels
{
    public partial class CoreViewModel : ObservableObject
    {
        private readonly IApiCoreService _apiCoreService;

        [ObservableProperty]
        private bool _isLoading = false;

        [ObservableProperty]
        private bool _isEnabled = true;

        [ObservableProperty]
        private string _searchText;

        [ObservableProperty]
        private ObservableCollection<CoreDTO> _coreList = new();

        private List<CoreDTO> _allCoreList = new();

        [RelayCommand]
        private Task AddTask() => Shell.Current.GoToAsync($"chat");

        public ICommand ClearCommand { get; }
        public ICommand DownloadCommand { get; }
        public ICommand PasswordDialogCommand { get; }
        public ICommand SearchTextCommand { get; }

        public CoreViewModel(IApiCoreService apiCoreService)
        {
            ClearCommand = new RelayCommand(OnClearCoreList);
            DownloadCommand = new RelayCommand(OnDownload);
            PasswordDialogCommand = new RelayCommand(OnShowPasswordDialog);
            SearchTextCommand = new RelayCommand<string>(OnSearchTextChanged);
            _apiCoreService = apiCoreService;
        }

        private async void OnClearCoreList()
        {
            CoreList.Clear();
        }

        private async void OnDownload()
        {
            Disable();
            CoreList.Clear();

            var loggedUser = await Storage.GetUser();

            var coreRequest = new CoreRequest<object>
            {
                IdUser = loggedUser.IdUser,
                SqlToken = loggedUser.SqlToken,
            };

            var responseApi = await _apiCoreService.GetAllAsync(coreRequest, loggedUser.ApiToken);

            if (!responseApi.IsSucces)
            {
                await Shell.Current.DisplayAlert($"Error: {responseApi.StatusCode}", responseApi.Message, "OK");

                Enable();
                return;
            }

            CoreList = new ObservableCollection<CoreDTO>(responseApi.Data);
            _allCoreList = new List<CoreDTO>(responseApi.Data);

            Enable();
        }

        private async void OnShowPasswordDialog()
        {
            var popup = new PasswordPopup();
            popup.PasswordSubmitted += OnPasswordEntered;
            Application.Current.MainPage.ShowPopup(popup);
        }

        private void OnPasswordEntered(object sender, string password)
        {
            _ = HandlePasswordAsync(password);
        }

        private async Task HandlePasswordAsync(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return;
            Disable();

            var (coreRequest, apiKeyToken) = await GetCoreRequestToken<object>(null);
            coreRequest.Password = password;

            var responseApi = await _apiCoreService.GetIVAsync(coreRequest, apiKeyToken);

            if (!responseApi.IsSucces)
            {
                await Shell.Current.DisplayAlert($"Error: ${responseApi.StatusCode}", responseApi.Message, "OK");

                Enable();
                return;
            }

            if (CoreList.Count > 0 && !IsBase64String(CoreList[0].Data01))
            {
                await Shell.Current.DisplayAlert("Error", "La Lista ya está Descifrado.", "Ok");

                Enable();
                return;
            }

            var codex = new EncryptionUtil();

            CoreList = new ObservableCollection<CoreDTO>(
                CoreList
                    .Select(item => codex.DecryptData(item, password, responseApi.Data.IV))
                    .OrderBy(x => x.Data01)
            );

            Enable();
        }

        partial void OnSearchTextChanged(string value)
        {
            Console.WriteLine($"Texto buscado: {value}");

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                CoreList.Clear();
                foreach (var item in _allCoreList)
                {
                    CoreList.Add(item);
                }
            }
            else
            {
                var filtrados = _allCoreList
                    .Where(x => x.Data01.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

                CoreList.Clear();
                foreach (var item in filtrados)
                {
                    CoreList.Add(item);
                }
            }
        }

        private async Task<(CoreRequest<T>, string)> GetCoreRequestToken<T>(T obj)
        {
            var loggedUser = await Storage.GetUser();

            var apiKeyToken = loggedUser.ApiToken;

            var coreRequest = new CoreRequest<T>
            {
                IdUser = loggedUser.IdUser,
                SqlToken = loggedUser.SqlToken,
                CoreData = obj
            };

            return (coreRequest, apiKeyToken);
        }

        private bool IsBase64String(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                return false;
            }

            Span<byte> buffer = new Span<byte>(new byte[base64String.Length]);
            return Convert.TryFromBase64String(base64String, buffer, out _);
        }

        private void Enable()
        {
            IsLoading = false;
            IsEnabled = true;
        }

        private void Disable()
        {
            IsLoading = true;
            IsEnabled = false;
        }
    }
}
