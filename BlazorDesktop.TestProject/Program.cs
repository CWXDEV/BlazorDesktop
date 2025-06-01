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
            Width = 800,
            Height = 600,
            ClientAreaTransparent = false,
            DarkMode = true,
            Frameless = true
        };
        
        var app = new BlazorApp(appOptions);
        
        app.Run();
    }
}
