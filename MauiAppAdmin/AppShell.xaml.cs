using MauiAppAdmin.Services;
using MauiAppAdmin.Views;

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

            var loginPage = _serviceProvider.GetRequiredService<LoginPage>();
            App.Current.MainPage = new NavigationPage(loginPage);
        }
    }
}
