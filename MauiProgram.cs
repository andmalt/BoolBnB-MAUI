﻿using BoolBnB_MAUI.Pages;
using BoolBnB_MAUI.Services;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace BoolBnB_MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<AuthService>();

            return builder.Build();
        }
    }
}