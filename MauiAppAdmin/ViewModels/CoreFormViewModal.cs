using ClassLibrary.Models.DTOs;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiAppAdmin.ViewModels
{
    public partial class CoreFormViewModal : ObservableObject
    {
        private TaskCompletionSource<(CoreDTO, string)> _taskCompletionSource;

        public CoreFormViewModal()
        {
            // Aquí se inicia la TaskCompletionSource para la comunicación con la página anterior
            _taskCompletionSource = new TaskCompletionSource<(CoreDTO, string)>();
        }
    }
}
