using BlazorDesktop.Window;
using BlazorDesktop.Window.Options;

namespace BlazorDesktop.Blazor;

public class BlazorApp
{
    public AppOptions? AppOptions { get; set; }
    
    public BlazorApp()
    {
        AppOptions = new AppOptions();
    }

    public BlazorApp(AppOptions appOptions)
    {
        AppOptions = appOptions;
    }
    
    public async Task Run()
    {
        var thread = new Thread(async () =>
        {
            var manager = new WindowManager();
            await manager.CreateWindow(AppOptions);
            manager.Run();
        });

        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
    }
}
