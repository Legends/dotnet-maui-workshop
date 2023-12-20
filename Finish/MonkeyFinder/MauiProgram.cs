using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.PlatformConfiguration;
using MonkeyFinder.Interfaces;
using MonkeyFinder.Services;
using MonkeyFinder.View;

namespace MonkeyFinder;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});
#if DEBUG
		builder.Logging.AddDebug();
#endif

		builder.Services.AddSingleton<IShellService>(new ShellService());

		builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
		builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
		builder.Services.AddSingleton<IMap>(Map.Default);

		#region Register platform-specific services

#if ANDROID
			builder.Services.AddSingleton<IPlatformSpecificService, MonkeyFinder.Platforms.Android.Services.PlatformSpecificService>();
#endif

#if IOS
		builder.Services.AddSingleton<IPlatformSpecificService, MonkeyFinder.Platforms.iOS.Services.PlatformSpecificService>();
#endif

#if WINDOWS
		builder.Services.AddSingleton<IPlatformSpecificService, MonkeyFinder.Platforms.Windows.Services.PlatformSpecificService>();
#endif
#if MACCATALYST
		builder.Services.AddSingleton<IPlatformSpecificService, MonkeyFinder.Platforms.MacCatalyst.Services.PlatformSpecificService>();
#endif

		#endregion

		builder.Services.AddSingleton<MonkeyService>();
		builder.Services.AddSingleton<MonkeysViewModel>();
		builder.Services.AddSingleton<MainPage>();

		builder.Services.AddTransient<MonkeyDetailsViewModel>();
		builder.Services.AddTransient<DetailsPage>();



		return builder.Build();
	}
}
