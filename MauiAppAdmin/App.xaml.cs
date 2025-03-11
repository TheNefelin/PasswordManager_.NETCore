using MauiAppAdmin.Views;

namespace MauiAppAdmin
{
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;

            App.Current.UserAppTheme = AppTheme.Dark;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var loginPage = _serviceProvider.GetRequiredService<LoginPage>();

            return new Window(new NavigationPage(loginPage));
        }
    }
}