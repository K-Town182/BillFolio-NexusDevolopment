using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;

namespace BillFolio
{
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
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

            // Initialize the database
            // DatabaseHelper.InitializeAsync();

            return builder.Build();
		}
	}
}
