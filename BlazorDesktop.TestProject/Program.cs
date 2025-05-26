using BlazorDesktop.Blazor;
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
            Width = 1280,
            Height = 1024,
            DisableResize = false,
            Fullscreen = false,
            Frameless = false,
            MinWidth = 0,
            MinHeight = 0,
            MaxWidth = 0,
            MaxHeight = 0,
            StartHidden = false,
            HideWindowOnClose = false,
            AlwaysOnTop = false,
            BackgroundColour = RGBA.NewRGBA(69, 69, 69, 255)
        };
        
        var app = new BlazorApp(appOptions);
        
        app.Run();
    }
}
