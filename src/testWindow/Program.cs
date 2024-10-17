using BlazorDesktop;
using BlazorDesktop.Models;
using BlazorDesktop.Options;

namespace TestWindow;

public class Program
{
    public static void Main()
    {
        AppOptions appOptions = new AppOptions
        {
            Title = "null",
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
            BackgroundColour = RGBA.NewRGBA(64, 64, 64, 255)
        };
        
        App app = new App(appOptions);
        
        app.Run();
    }
}