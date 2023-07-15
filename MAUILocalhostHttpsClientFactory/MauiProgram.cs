using Microsoft.Extensions.Logging;

namespace MAUILocalhostHttpsClientFactory;

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

        builder.Services.AddSingleton<IPlatformHttpMessageHandler>(_ =>
        {
#if ANDROID
            return new Platforms.Android.AndroidHttpMessageHandler();
#elif IOS
            return new Platforms.iOS.IosHttpMessageHandler();
#endif
        });

        builder.Services.AddHttpClient("maui-to-https-localhost", httpClient =>
        {
            var baseAddress =
                    DeviceInfo.Platform == DevicePlatform.Android
                        ? "https://10.0.2.2:12345"
                        : "https://localhost:12345";

            httpClient.BaseAddress = new Uri(baseAddress);
        })
            .ConfigureHttpMessageHandlerBuilder(builder =>
            {
                var platfromHttpMessageHandler = builder.Services.GetRequiredService<IPlatformHttpMessageHandler>();
                builder.PrimaryHandler = platfromHttpMessageHandler.GetHttpMessageHandler();
            });

        builder.Services.AddSingleton<MainPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

