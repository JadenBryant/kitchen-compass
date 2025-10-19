using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.LifecycleEvents;
using Plugin.Firebase.Auth;        // for CrossFirebaseAuth.Current
using CustomerFrontend;
// no Plugin.Firebase.Core needed if you use native init

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder()
            .UseMauiApp<App>();

        builder.ConfigureLifecycleEvents(events =>
        {
#if IOS
            events.AddiOS(iOS => iOS.WillFinishLaunching((_, __) =>
            {
                // Initialize Firebase using the native iOS binding
                global::Firebase.Core.App.Configure();   // reads GoogleService-Info.plist
                return false;
            }));
#endif
        });

        // DI for auth
        builder.Services.AddSingleton(_ => CrossFirebaseAuth.Current);

        return builder.Build();
    }
}