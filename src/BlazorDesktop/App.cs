using System.Drawing;
using BlazorDesktop.Models;
using BlazorDesktop.Options;

namespace BlazorDesktop;

public class App
{
    public AppOptions? AppOptions { get; set; }

    public App()
    {
        AppOptions = new AppOptions();
    }

    public App(AppOptions appOptions)
    {
        AppOptions = appOptions;
    }

    public async Task Run()
    {
        WindowManager manager = new WindowManager();
        await manager.CreateWindow(AppOptions);
        manager.InitializeAsync(AppOptions.Handle.Value, "https://www.bing.com", new Rectangle(0, 0, AppOptions.Width, AppOptions.Height));
        manager.Run();
        
        // await WindowManager.CreateWindow(AppOptions);
        // WindowManager.InitializeAsync(AppOptions.Handle.Value, "https://www.bing.com", new Rectangle(0, 0, 1200, 800));
        // WindowManager.Run();
    }
}