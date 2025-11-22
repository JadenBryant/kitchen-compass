namespace CustomerFrontend;

using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;

#if ANDROID || IOS
using Plugin.Firebase.Auth;
#endif

#if ANDROID
using Plugin.Firebase.Core.Platforms.Android;
#elif IOS
using Plugin.Firebase.Core.Platforms.iOS;
#endif

public static class MauiProgram {
    public static MauiApp CreateMauiApp() {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if ANDROID || IOS
        builder.RegisterFirebaseServices();
#endif

        return builder.Build();
    }

#if ANDROID || IOS
    private static MauiAppBuilder RegisterFirebaseServices(this MauiAppBuilder builder) {
        builder.ConfigureLifecycleEvents(events => {
#if IOS
            events.AddiOS(iOS => iOS.WillFinishLaunching((_, __) => {
                CrossFirebase.Initialize();
                return false;
            }));
#elif ANDROID
            events.AddAndroid(android => android.OnCreate((activity, _) => {
                CrossFirebase.Initialize(activity);
            }));
#endif
        });

        return builder;
    }
#endif
}