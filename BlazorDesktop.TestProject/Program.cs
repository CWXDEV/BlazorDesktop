using BlazorDesktop.Blazor;
using BlazorDesktop.Window;
using BlazorDesktop.Window.Models;
using BlazorDesktop.Window.Options;

namespace BlazorDesktop.TestProject;

public class Program
{
    public static void Main(string[] args)
    {
        var appOptions = new AppOptions
        {
            Title = "Testing Project for creating windows",
            Width = 800,
            Height = 600,
            StartPosition = StartPosition.Center,
            Top = 99,
            Left = 22,
            AlwaysOnTop = false,
            MinHeight = 600,
            MinWidth = 800,
            MaxHeight = 1000,
            MaxWidth = 1200,
            ClientAreaTransparent = false,
            DarkMode = true,
            CustomTitleBar = false,
            TitleBarColor = WindowManager.RGBToUInt(69, 69, 69),
            TitleBorderColor = WindowManager.RGBToUInt(69, 69, 69),
            TitleTextColor = WindowManager.RGBToUInt(255, 255, 255),
            HideTitleBar = true
        };
        
        var app = new BlazorApp(appOptions);
        
        app.Run();
    }
}
