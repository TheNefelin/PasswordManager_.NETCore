using ClassLibrary.Models.DTOs;
using Newtonsoft.Json;

namespace MauiAppAdmin.Views;

public partial class CoreFormPage : ContentPage
{
    private TaskCompletionSource<(CoreDTO, string)> _taskCompletionSource;
    public string CoreDTOJson { get; set; }

    public CoreFormPage()
	{
		InitializeComponent();

        // Inicializar TaskCompletionSource
        _taskCompletionSource = new TaskCompletionSource<(CoreDTO, string)>();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Shell.SetNavBarIsVisible(this, false); // Oculta la barra de navegación

        // Recibir el parámetro de la URL (coreDTO) y deserializarlo
        if (!string.IsNullOrEmpty(CoreDTOJson))
        {
            var coreDTO = JsonConvert.DeserializeObject<CoreDTO>(Uri.UnescapeDataString(CoreDTOJson));

            // Usar el objeto coreDTO, por ejemplo, procesar algo
            var password = "dummyPassword";  // Aquí puedes obtener tu valor real

            // Pasar el resultado de vuelta a la página origen
            _taskCompletionSource?.SetResult((coreDTO, password));
        }
    }
}