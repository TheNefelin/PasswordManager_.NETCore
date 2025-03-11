using MauiAppTry.Views;

namespace MauiAppTry
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            await PerformLogout();
        }

        public async Task PerformLogout()
        {
            if (Application.Current?.Windows?.Count > 0)
            {
                Application.Current.Windows[0].Page = new NavigationPage(new LoginPage());
            }
        }
    }
}
