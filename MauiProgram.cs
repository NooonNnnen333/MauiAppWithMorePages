using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Povtorenie333;
using Povtorenie333.System;

namespace Povtorenie333;

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

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<SystemPg111>();

        builder.Services.AddTransient<HouseCreatePage>();
        builder.Services.AddTransient<System.HouseCreateSys>();
        
        builder.Services.AddTransient<OfficeBuildCreatePage>();
        builder.Services.AddTransient<System.OfficeBuildCreateSys>();
        
        builder.Services.AddTransient<DetailObjectPage>();
        builder.Services.AddTransient<System.DetObjPg>();
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}