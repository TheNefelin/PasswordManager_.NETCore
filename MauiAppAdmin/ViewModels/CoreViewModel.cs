using ClassLibrary.Models;
using ClassLibrary.Models.DTOs;
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
        private bool isLoading = false;

        [ObservableProperty]
        private ObservableCollection<CoreDTO> _coreList = new();

        public ICommand DownloadCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand PasswordDialogCommand { get; }

        public CoreViewModel(IApiCoreService apiCoreService)
        {
            DownloadCommand = new RelayCommand(OnDownload);
            ClearCommand = new RelayCommand(OnClearCoreList);
            PasswordDialogCommand = new RelayCommand(OnShowPasswordDialog);
            _apiCoreService = apiCoreService;
        }

        private async void OnClearCoreList()
        {
            CoreList.Clear();
        }

        private async void OnDownload()
        {
            IsLoading = true;

            var loggedUser = await Storage.GetUser();

            var coreRequest = new CoreRequest<object>
            {
                IdUser = loggedUser.IdUser,
                SqlToken = loggedUser.SqlToken,
            };

            var responseApi = await _apiCoreService.GetAllAsync(coreRequest, loggedUser.ApiToken);

            if (!responseApi.IsSucces)
            {
                return;
            }

            CoreList.Clear();

            foreach (CoreDTO item in responseApi.Data)
            {
                CoreList.Add(item);
            }

            IsLoading = false;
        }

        private void OnShowPasswordDialog()
        {
            var popup = new PasswordPopup();
            popup.PasswordSubmitted += OnPasswordEntered;
            Application.Current.MainPage.ShowPopup(popup);
        }

        private void OnPasswordEntered(object sender, string password)
        {
            var ps = password;
        }
    }
}
