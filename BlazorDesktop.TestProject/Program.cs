using BlazorDesktop.Blazor;
using BlazorDesktop.Window;
using BlazorDesktop.Window.Models;
using BlazorDesktop.Window.Options;
using BlazorDesktop.Window.Helpers;

namespace BlazorDesktop.TestProject;

public class Program
{
    public static void Main(string[] args)
    {
        var appOptions = new AppOptions
        {
            Title = "Testing Project for creating windows",
            WindowStyle = WindowStyle.FramelessWindowWithBorder,
            DarkMode = false,
            TranslucentWindow = true,
            WindowBackdropType = WindowBackdropType.Acrylic
        };

        var app = new BlazorApp(appOptions);

        app.Run();
    }
}
