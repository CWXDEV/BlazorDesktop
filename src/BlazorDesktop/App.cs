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

    /// <summary>
    /// Runs the application in a new thread with the specified <see cref="AppOptions"/>.
    /// </summary>
    /// <remarks>
    /// This method creates a new <see cref="Thread"/>, sets its apartment state to STA,
    /// and starts the thread. The thread initializes a new <see cref="WindowManager"/>,
    /// creates a new window with the specified <see cref="AppOptions"/>,
    /// initializes the window with the specified URL, and runs the message loop.
    /// </remarks>
    public async Task Run()
    {
        Thread thread = new Thread(async () =>
        {
            WindowManager manager = new WindowManager();
            await manager.CreateWindow(AppOptions);
            manager.InitializeAsync(AppOptions.Handle.Value, "https://www.bing.com");
            manager.Run();
        });

        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
    }
}