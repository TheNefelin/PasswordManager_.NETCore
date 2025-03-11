using CommunityToolkit.Maui;
using MauiAppAdmin.Services;
using MauiAppAdmin.ViewModels;
using MauiAppAdmin.Views;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Toolkit.Hosting;

namespace MauiAppAdmin;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureSyncfusionToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton<HttpClient>(new HttpClient()
        {
            BaseAddress = new Uri("https://dragonra.bsite.net")
        });

        string apiKey = "Esmerilemelo-777";

        builder.Services.AddSingleton<IApiAuthService>(provider =>
        {
            var httpClient = provider.GetRequiredService<HttpClient>();
            return new ApiAuthService(httpClient, apiKey);
        });

        builder.Services.AddSingleton<IApiCoreService>(provider =>
        {
            var httpClient = provider.GetRequiredService<HttpClient>();
            return new ApiCoreService(httpClient, apiKey);
        });

        builder.Services.AddTransient<AppShell>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<RegisterPageModel>();
        
        builder.Services.AddTransient<CoreViewModel>();
        builder.Services.AddTransient<ProfileViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
        builder.Services.AddLogging(configure => configure.AddDebug());
#endif

        return builder.Build();
	}
}
