using BlazorDesktop;
using BlazorDesktop.Models;
using BlazorDesktop.Options;

namespace TestWindow;

public class Program
{
    [STAThread]
    public static void Main()
    {
        AppOptions appOptions = new AppOptions
        {
            Title = "null",
            Height = 800,
            Width = 600,
            DisableResize = false,
            Fullscreen = false,
            Frameless = false,
            MinHeight = 0,
            MinWidth = 0,
            MaxHeight = 0,
            MaxWidth = 0,
            StartHidden = false,
            HideWindowOnClose = false,
            AlwaysOnTop = false,
            BackgroundColour = RGBA.NewRGBA(64, 64, 64, 255)
        };
        
        App app = new App(appOptions);
        
        app.Run();
    }
}