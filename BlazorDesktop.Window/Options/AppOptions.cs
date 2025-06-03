using System.ComponentModel.DataAnnotations;
using BlazorDesktop.Window.Models;

namespace BlazorDesktop.Window.Options;

public class AppOptions
{
    public IntPtr? Handle { get; set; }
    public IntPtr? WebviewHandle { get; set; }

    public string Title { get; set; } = "Blazor Desktop App";

    public bool DisableResize { get; set; }
    public bool Fullscreen { get; set; }
    public bool StartHidden { get; set; }
    public bool HideWindowOnClose { get; set; }
    public bool DarkMode { get; set; }
    public bool TranslucentWindow { get; set; }
    public bool AlwaysOnTop { get; set; }

    public int Height { get; set; } = 600;
    public int Width { get; set; } = 800;
    public int? MinHeight { get; set; }
    public int? MinWidth { get; set; }
    public int? MaxHeight { get; set; }
    public int? MaxWidth { get; set; }
    public int? Top { get; set; } = 0;
    public int? Left { get; set; } = 0;

    public StartPosition StartPosition { get; set; } = StartPosition.Center;
    public WindowStyle WindowStyle { get; set; } = WindowStyle.DefaultWindow;
    public WindowState WindowState { get; set; } = WindowState.Normal;
    public WindowBackdropType WindowBackdropType { get; set; } = WindowBackdropType.Auto;
    public StartingWindowState StartingWindowState { get; set; } = StartingWindowState.Visible;

    public RGB? BackgroundColour { get; set; }
    public RGB? TitleTextColor { get; set; } = new(0, 0, 0);
    public RGB? TitleBarColor { get; set; } = new(255, 255, 255);
    public RGB? TitleBorderColor { get; set; } = new(255, 255, 255);
}

public enum StartPosition
{
    Center,
    Manual
}

public enum WindowStyle
{
    DefaultWindow,
    FramelessWindow,
    FramelessWindowWithBorder,
    WindowWithCustomTheme,
    Mica
}

public enum WindowState
{
    Normal,
    Maximized,
    Minimized
}

public enum StartingWindowState
{
    Hidden,
    Visible,
    Fullscreen
}

public enum WindowBackdropType
{
    Auto,
    None,
    Mica,
    Acrylic,
    Tabbed,
}

public record RGB
{
    public byte Red { get; set; }
    public byte Green { get; set; }
    public byte Blue { get; set; }

    public RGB(byte r, byte g, byte b)
    {
        Red = r;
        Green = g;
        Blue = b;
    }

    public RGB()
    {

    }
}
