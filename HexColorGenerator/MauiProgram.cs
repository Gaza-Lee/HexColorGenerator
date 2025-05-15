using Microsoft.Extensions.Logging;
using Microsoft.Maui.Handlers;
using CommunityToolkit.Maui;


namespace HexColorGenerator
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
                    fonts.AddFont("Ubuntu-Regular.ttf", "UbuntuRegular");
                    fonts.AddFont("Ubuntu-Semibold.ttf", "UbuntuSemibold");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcon");
                });


            //Remove Underlin in Entry Field
            EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.Background = null;
#endif
            });
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
