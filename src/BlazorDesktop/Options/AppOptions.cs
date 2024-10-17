using BlazorDesktop.Models;

namespace BlazorDesktop.Options;

public class AppOptions
{
    public IntPtr? Handle { get; set; } = null;
    public IntPtr? WebviewHandle { get; set; } = null;
    public string Title { get; set; } = string.Empty;
    public int Height { get; set; } = 800;
    public int Width { get; set; } = 600;
    public bool DisableResize { get; set; } = false;
    public bool Fullscreen { get; set; } = false;
    public bool Frameless { get; set; } = false;
    public int MinHeight { get; set; } = 0;
    public int MinWidth { get; set; } = 0;
    public int MaxHeight { get; set; } = 0;
    public int MaxWidth { get; set; } = 0;
    public bool StartHidden { get; set; } = false;
    public bool HideWindowOnClose { get; set; } = false;
    public bool AlwaysOnTop { get; set; } = false;
    public RGBA BackgroundColour { get; set; } = new RGBA() { Red = 0, Green = 0, Blue = 0, Alpha = 255 };
    
    // Extra's in C:\Users\craig\go\pkg\mod\github.com\wailsapp\wails\v2@v2.9.2\pkg\options\options.go
}