using Microsoft.Extensions.Logging;
using CommunityToolkit;
using CommunityToolkit.Maui;
using TechnicianAllInOne.Data;

namespace TechnicianAllInOne
{
    //public partial class MauiProgram
    public static class MauiProgram

    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitCamera()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Cache Pages
            //builder.Services.AddSingleton<LocalDBService>();
            //builder.Services.AddSingleton<MainPage>();
            //builder.Services.AddSingleton<MissedServicePage>();
            //builder.Services.AddSingleton<SignUp>();
            //builder.Services.AddSingleton<TechnicianView>();


            builder.Services.AddSingleton<LocalDBService>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MissedServicePage>();
            builder.Services.AddTransient<SignUp>();
            builder.Services.AddTransient<TechnicianView>();
            builder.Services.AddTransient<ChangedServicePage>();
            builder.Services.AddTransient<ExpenseReportPage>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
