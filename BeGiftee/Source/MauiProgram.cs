using BeGiftee.Source.Services.Network;
using BeGiftee.Source.Services.Network.Clients;
using Microsoft.Extensions.Logging;

namespace BeGiftee.Source
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .RegisterAppServices()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build(); ; 
        }

        public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddSingleton<HttpService>();
            mauiAppBuilder.Services.AddSingleton<IAuthenticationService, HttpAuthenticationService>();
            return mauiAppBuilder;
        }
    }
}
