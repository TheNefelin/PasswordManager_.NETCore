using MauiAppAdmin.Services;
using MauiAppAdmin.Views;
using System.Threading.Tasks;

namespace MauiAppAdmin
{
    public partial class AppShell : Shell
    {
        private readonly IServiceProvider _serviceProvider;

        public AppShell(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            await PerformLogout();
        }

        public async Task PerformLogout()
        {
            await Storage.RemoveUser();

            var currentWindow = Application.Current!.Windows.FirstOrDefault();
            if (currentWindow != null)
            {
                Application.Current!.CloseWindow(currentWindow);
            }

            var loginPage = _serviceProvider.GetRequiredService<LoginPage>();
            Application.Current!.OpenWindow(new Window(loginPage));
        }
    }
}
