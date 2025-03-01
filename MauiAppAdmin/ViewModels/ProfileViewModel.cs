using ClassLibrary.Models.DTOs;
using CommunityToolkit.Mvvm.ComponentModel;
using MauiAppAdmin.Services;
using System.Timers;

namespace MauiAppAdmin.ViewModels
{
    public partial class ProfileViewModel : ObservableObject
    {
        [ObservableProperty]
        private AuthLogged _auth = new();

        [ObservableProperty]
        private string _loginDate;

        [ObservableProperty]
        private string _countDown;

        private System.Timers.Timer _timer;
        private TimeSpan _expireTime;

        public ProfileViewModel()
        {
            LoadUserAsync();
            StartCountdown();
        }

        private async void LoadUserAsync()
        {
            Auth = await Storage.GetUser();
            LoginDate = await Storage.GetLoginDate();

            if (int.TryParse(Auth.ExpireMin, out int expireMinutes))
            {
                _expireTime = TimeSpan.FromMinutes(expireMinutes);
            } 
            else
            {
                _expireTime = TimeSpan.FromMinutes(1);
            }
        }

        private void StartCountdown()
        {
            _timer = new System.Timers.Timer(1000); // Establecer el temporizador para que se ejecute cada segundo
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true; // Reiniciar el temporizador
            _timer.Enabled = true; // Habilitar el temporizador
        }
        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            // Actualizar la cuenta regresiva en el hilo de la UI
            Application.Current.Dispatcher.Dispatch(() =>
            {
                UpdateCountDown();
            });
        }

        private void UpdateCountDown()
        {
            // Calcular el tiempo restante
            if (_expireTime.TotalSeconds > 0)
            {
                _expireTime -= TimeSpan.FromSeconds(1);
                CountDown = $"{_expireTime.Minutes:D2}:{_expireTime.Seconds:D2} minutos restantes"; // Actualizar la propiedad CountDown
            }
            else
            {
                CountDown = "Token expirado."; // Mensaje de expiración
                _timer.Stop(); // Detener el temporizador
                _timer.Dispose(); // Liberar recursos del temporizador

                // Llamar al logout automáticamente
                var shell = Application.Current.MainPage as AppShell;
                shell?.PerformLogout(); // Invoca el logout desde AppShell
            }
        }
    }
}
